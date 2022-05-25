using PreSchool.Application.Events;
using PreSchool.Application.Exceptions;
using PreSchool.Application.HelperClasses;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Models.AppUsers.Queries;
using PreSchool.Application.Services.Addresses.Models.Filters;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Application.Services.AppUsers.Models.Queries;
using PreSchool.Application.Services.Emails;
using PreSchool.Application.Services.SMSSenders;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Entities.AppUsers.Events;
using PreSchool.Data.Entities.Stores;
using PreSchool.Data.Enumerations;
using PreSchool.Data.Events;
using PreSchool.EmailTemplates.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Application.Services.Files;

namespace PreSchool.Application.Services.AppUsers
{
    public class AppUserService : IAppUserService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly IHostingEnvironmentService _hostingEnvironment;
        private readonly ICurrentUserService _currentUserService;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IAppFeatureService _featureService;
        private readonly IEventPublisher _eventPublisher;
        private readonly AppSettings _appSettings;
        private readonly ISMSSenderService _sMSSenderService;
        private readonly ICodeService _codeService;
        private readonly IFileService _fileService;

        public AppUserService(
            IApplicationDbContext context,
            IDateTime dateTime,
            IHostingEnvironmentService hostingEnvironment,
            IOptions<AppSettings> appSettings,
            ICurrentUserService currentUserService,
            IJwtService jwtService,
            IEmailService emailService,
            IAppFeatureService featureService,
           IEventPublisher eventPublisher,
         ISMSSenderService sMSSenderService,
            ICodeService codeService,
            IFileService fileService

            )
        {
            _context = context;
            _dateTime = dateTime;
            _hostingEnvironment = hostingEnvironment;
            _currentUserService = currentUserService;
            _jwtService = jwtService;
            _emailService = emailService;
            _featureService = featureService;
            _eventPublisher = eventPublisher;
            _appSettings = appSettings.Value;
            _sMSSenderService = sMSSenderService;
            _codeService = codeService;
            _fileService = fileService;


        }


        public async Task<int> InsertInternalUser(InsertAppUser appUserCommand)
        {

            if (!_currentUserService.HavePermission(Permissions.CreateInternalUser))
                throw new ForbiddenException();

            // Remove white spaces
            appUserCommand.Username = appUserCommand.Username?.Trim();

            // Check if the username is already exists
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(u => u.Username == appUserCommand.Username);

            if (user != null)
                throw new BadRequestException("Username is already taken");

            // Check for email for same app user type
            var email = await _context.AppUsers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Email == appUserCommand.Email);

            if (email != null)
                throw new BadRequestException("Email is already registered");


            // Check for phone number for same app user type
            var phoneNumber = await _context.AppUsers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.PhoneNumber == appUserCommand.PhoneNumber && u.AppUserTypeId == appUserCommand.AppUserTypeId);
            if (phoneNumber != null)
                throw new BadRequestException("Phone number is already registered");

            // TODO: Check if passed department and roll is of usertype

            // Hash the passowrd
            byte[] passwordHash, passwordSalt;
            PasswordHelper.CreatePasswordHash(appUserCommand.Password, out passwordHash, out passwordSalt);



            // Check if the role is super admin or not
            if (appUserCommand.RoleIds.Contains((int)ReservedRoles.SuperAdmin))
                throw new BadRequestException("Invalid role.");

            // Check if the roles are valid
            var validRoles = await _context.Roles.Select(x => x.Id).ToListAsync();
            if (!appUserCommand.RoleIds.All(x => validRoles.Contains(x)))
                throw new BadRequestException("Invalid role.");

            var appUser = new AppUser()
            {
                Username = appUserCommand.Username,
                Active = true,
                FailedLoginAttempts = 0,
                IsDeleted = false,
                IsSystemAccount = false,
                RequireReLogin = true,
                AdminComment = appUserCommand.AdminComment,
                CannotLoginUntilDate = appUserCommand.CannotLoginUntilDate,
                SystemName = appUserCommand.Username,
                Email = appUserCommand.Email,
                AppUserTypeId = appUserCommand.AppUserTypeId,
                RegisteredInStoreId = _currentUserService.StoreId ?? null,
                FirstName = appUserCommand.FirstName,
                LastName = appUserCommand.LastName,
                JobTitle = appUserCommand.JobTitle,
                MiddleName = appUserCommand.MiddleName,
                PhoneNumber = appUserCommand.PhoneNumber,
                LimitedToStores = false,
                AppUserRoles = appUserCommand.RoleIds.Select(roleId => new AppUserRole
                {
                    IsDeleted = false,
                    RoleId = roleId
                }).ToList(),
                AppUserPasswords = new List<AppUserPassword> { new AppUserPassword
                {
                    CreatedOn = _dateTime.Now,
                    EnablePasswordLifetime = appUserCommand.EnablePasswordLifetime,
                    IsCurrent = true,
                    PasswordExpiredOn = appUserCommand.PasswordExpiredOn,
                    RequiredPasswordChange = appUserCommand.RequiredPasswordChange,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                } }

            };

            // Address
            if (appUserCommand.Address != null)
            {
                if (appUser.Address == null)
                    appUser.Address = new Data.Entities.Address();

                appUser.Address.Email = appUserCommand.Address.Email;
                appUser.Address.CountryId = appUserCommand.Address.CountryId == 0 ? null : appUserCommand.Address.CountryId;
                appUser.Address.CountryProvinceId = appUserCommand.Address.CountryProvinceId == 0 ? null : appUserCommand.Address.CountryProvinceId;
                appUser.Address.CityId = appUserCommand.Address.CityId;
                appUser.Address.RegionId = appUserCommand.Address.RegionId;
                appUser.Address.Address1 = appUserCommand.Address.Address1;
                appUser.Address.Address2 = appUserCommand.Address.Address2;
                appUser.Address.ZipPostalCode = appUserCommand.Address.ZipPostalCode;
                appUser.Address.PhoneNumber = appUserCommand.Address.PhoneNumber;
                appUser.Address.SecondaryPhoneNumber = appUserCommand.Address.SecondaryPhoneNumber;
                appUser.Address.FaxNumber = appUserCommand.Address.FaxNumber;
                appUser.Address.MapLink = appUserCommand.Address.MapLink; ;
            }
            _context.AppUsers.Add(appUser);

            await _context.SaveChangesAsync();

            // Send welcome internal user email
            _emailService.WelcomeInternalUser(new WelcomeInternalUserEmailViewModel(appUserCommand.Username, appUserCommand.Email));
            _eventPublisher.Publish(new AppUserRegisteredEvent(appUser));

            return appUser.Id;

        }

        public async Task<bool> UpdateAppUser(UpdateAppUser appUserCommand)
        {
            // Get AppUser
            var appUser = await _context.AppUsers
                         .Include(a => a.Address)
                         .FirstOrDefaultAsync(a => a.Id == appUserCommand.Id);

            if (appUser == null)
                throw new NotFoundException("User not found");

            if (appUser.IsSystemAccount)
                throw new BadRequestException("System account user cannot be modified");

            //emailaddress
            if (appUser.Email != appUserCommand.Email)
            {
                var emailAddressExist = await _context.AppUsers
                        .FirstOrDefaultAsync(u => u.Email == appUserCommand.Email);
                if (emailAddressExist != null)
                    throw new BadRequestException("Invalid", "Email already existed.");
            }

            // Check for phone number is change
            if (appUser.PhoneNumber != appUserCommand.PhoneNumber)
            {
                var phoneNumber = await _context.AppUsers
                      .FirstOrDefaultAsync(u => u.PhoneNumber == appUserCommand.PhoneNumber && u.AppUserTypeId == appUser.AppUserTypeId);
                if (phoneNumber != null)
                    throw new BadRequestException("Phone number is already registered");
            }

            appUser.AdminComment = appUserCommand.AdminComment;
            appUser.FirstName = appUserCommand.FirstName;
            appUser.LastName = appUserCommand.LastName;
            appUser.JobTitle = appUserCommand.JobTitle;
            appUser.MiddleName = appUserCommand.MiddleName;
            appUser.PhoneNumber = appUserCommand.PhoneNumber;
            appUser.Active = appUserCommand.IsActive;
            appUser.Email = appUserCommand.Email;

            // Address
            if (appUserCommand.Address != null)
            {
                if (appUser.Address == null)
                    appUser.Address = new Data.Entities.Address();

                appUser.Address.Email = appUserCommand.Address.Email;
                appUser.Address.CountryId = appUserCommand.Address.CountryId == 0 ? null : appUserCommand.Address.CountryId;
                appUser.Address.CountryProvinceId = appUserCommand.Address.CountryProvinceId == 0 ? null : appUserCommand.Address.CountryProvinceId;
                appUser.Address.CityId = appUserCommand.Address.CityId;
                appUser.Address.RegionId = appUserCommand.Address.RegionId;
                appUser.Address.Address1 = appUserCommand.Address.Address1;
                appUser.Address.Address2 = appUserCommand.Address.Address2;
                appUser.Address.ZipPostalCode = appUserCommand.Address.ZipPostalCode;
                appUser.Address.PhoneNumber = appUserCommand.Address.PhoneNumber;
                appUser.Address.SecondaryPhoneNumber = appUserCommand.Address.SecondaryPhoneNumber;
                appUser.Address.FaxNumber = appUserCommand.Address.FaxNumber;
                appUser.Address.MapLink = appUserCommand.Address.MapLink;
            }
            var isUpdated = (await _context.SaveChangesAsync() > 0);

            if (isUpdated)
                _eventPublisher.Publish(new AppUserUpdatedEvent(appUser));

            return isUpdated;
        }

        #region Academic
        public async Task<int> InsertUpdateInternalUserAcademic(InsertUpdateInternalUserAcademic acadamicCmd)
        {
            var appUser = await _context.AppUsers
                     .Include(a => a.Address)
                     .FirstOrDefaultAsync(a => a.Id == acadamicCmd.AppUserId);

            if (appUser == null)
                throw new NotFoundException("User not found");

            if (acadamicCmd.Id == 0)
            {
                var newInternalUserAcademic = new InternalUserAcademic
                {
                    Action = acadamicCmd.Action,
                    AppUserId = acadamicCmd.AppUserId,
                    Division = acadamicCmd.Division,
                    Faculty = acadamicCmd.Faculty,
                    Level = acadamicCmd.Level,
                    PassedYear = acadamicCmd.PassedYear,
                    Specialization = acadamicCmd.Specialization,
                    University = acadamicCmd.University,
                };
                _context.InternalUserAcademics.Add(newInternalUserAcademic);
                await _context.SaveChangesAsync();
                return newInternalUserAcademic.Id;
            }
            else
            {
                var existing = await _context.InternalUserAcademics
                                      .FirstOrDefaultAsync(i => i.Id == acadamicCmd.Id);
                if (existing == null)
                    throw new NotFoundException("acadamic not found.");

                existing.Action = acadamicCmd.Action;
                existing.AppUserId = acadamicCmd.AppUserId;
                existing.Division = acadamicCmd.Division;
                existing.Faculty = acadamicCmd.Faculty;
                existing.Level = acadamicCmd.Level;
                existing.PassedYear = acadamicCmd.PassedYear;
                existing.Specialization = acadamicCmd.Specialization;
                existing.University = acadamicCmd.University;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<bool> DeleteAcadamic(int appUserId, int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var internalUserAcademic = await _context.InternalUserAcademics
              .FirstOrDefaultAsync(s => s.Id == id && s.AppUserId == appUserId);
            if (internalUserAcademic == null)
                throw new BadRequestException("Invalid internalUser Academic.");

            internalUserAcademic.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region EmergencyContacts
        public async Task<int> InsertUpdateInternalUserEmergencyContact(InsertUpdateInternalUserEmergencyContact appUserCommand)
        {
            var appUser = await _context.AppUsers
                     .Include(a => a.Address)
                     .FirstOrDefaultAsync(a => a.Id == appUserCommand.AppUserId);

            if (appUser == null)
                throw new NotFoundException("User not found");

            if (appUserCommand.Id == 0)
            {
                var newInternalUserEmergencyContact = new InternalUserEmergencyContact
                {
                    ContactNumber = appUserCommand.ContactNumber,
                    AppUserId = appUserCommand.AppUserId,
                    FirstName = appUserCommand.FirstName,
                    MiddleName = appUserCommand.MiddleName,
                    LastName = appUserCommand.LastName,
                    Relation = appUserCommand.Relation,
                };
                _context.InternalUserEmergencyContacts.Add(newInternalUserEmergencyContact);
                await _context.SaveChangesAsync();
                return newInternalUserEmergencyContact.Id;
            }
            else
            {
                var existing = await _context.InternalUserEmergencyContacts
                                      .FirstOrDefaultAsync(i => i.Id == appUserCommand.Id);
                if (existing == null)
                    throw new NotFoundException("Emergency Contact not found.");

                existing.ContactNumber = appUserCommand.ContactNumber;
                existing.AppUserId = appUserCommand.AppUserId;
                existing.FirstName = appUserCommand.FirstName;
                existing.MiddleName = appUserCommand.MiddleName;
                existing.LastName = appUserCommand.LastName;
                existing.Relation = appUserCommand.Relation;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<bool> DeleteEmergencyContacts(int appUserId, int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var internalUserEmergencyContact = await _context.InternalUserEmergencyContacts
                      .FirstOrDefaultAsync(s => s.Id == id && s.AppUserId == appUserId);
            if (internalUserEmergencyContact == null)
                throw new BadRequestException("Invalid internalUser Emergency Contacts.");

            internalUserEmergencyContact.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Experience
        public async Task<int> InsertUpdateInternalUserExperience(InsertUpdateInternalUserExperience appUserCommand)
        {
            var appUser = await _context.AppUsers
                     .Include(a => a.Address)
                     .FirstOrDefaultAsync(a => a.Id == appUserCommand.AppUserId);

            if (appUser == null)
                throw new NotFoundException("User not found");

            if (appUserCommand.Id == 0)
            {
                var newInternalUserExperience = new InternalUserExperience
                {
                    Address = appUserCommand.Address,
                    AppUserId = appUserCommand.AppUserId,
                    Designation = appUserCommand.Designation,
                    InstitutionName = appUserCommand.InstitutionName,
                    JoinDate = appUserCommand.JoinDate,
                    LeftDate = appUserCommand.LeftDate,
                    ReasonforLeaving = appUserCommand.ReasonforLeaving,
                };
                _context.InternalUserExperiences.Add(newInternalUserExperience);
                await _context.SaveChangesAsync();
                return newInternalUserExperience.Id;
            }
            else
            {
                var existing = await _context.InternalUserExperiences
                                      .FirstOrDefaultAsync(i => i.Id == appUserCommand.Id);
                if (existing == null)
                    throw new NotFoundException("Experiences not found.");

                existing.Address = appUserCommand.Address;
                existing.AppUserId = appUserCommand.AppUserId;
                existing.Designation = appUserCommand.Designation;
                existing.InstitutionName = appUserCommand.InstitutionName;
                existing.JoinDate = appUserCommand.JoinDate;
                existing.LeftDate = appUserCommand.LeftDate;
                existing.ReasonforLeaving = appUserCommand.ReasonforLeaving;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<bool> DeleteExperience(int appUserId, int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var internalUserExperiences = await _context.InternalUserExperiences
                                 .FirstOrDefaultAsync(s => s.Id == id && s.AppUserId == appUserId);
            if (internalUserExperiences == null)
                throw new BadRequestException("Invalid internalUser Experience.");

            internalUserExperiences.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Documents.
        public async Task<bool> InsertUpdateInternalUserDocuments(InsertUpdateInternalUserDocuments image)
        {
            var appUser = await _context.AppUsers
                .Include(a => a.Address)
                .FirstOrDefaultAsync(a => a.Id == image.appUserId);

            if (appUser == null)
                throw new NotFoundException("User not found");

            int? fileId = null;

            // Save the image if the product image is not null
            if (image?.image != null)
            {
                // Check if the file is image
                if (!image.image.IsImage())
                    throw new BadRequestException("Only image file is supported");

                fileId = await _fileService.InsertFile(new Files.Models.InsertFileCommand { EntityName = nameof(InternalUserDocuments), File = image.image });

            }

            if (image.id == 0)
            {
                if (fileId is null || fileId == 0)
                    throw new BadRequestException("Document file is required");
                appUser.InternalUserDocuments.Add(new InternalUserDocuments
                {
                    ImageId = fileId ?? 0,
                    AppUserId = image.appUserId,
                    DocumentIssuedFrom = image.documentIssuedFrom,
                    DocumentIssuedOn = image.documentIssuedOn,
                    DocumentName = image.documentName,
                    DocumentNumber = image.documentNumber,
                    DocumentType = image.documentType,

                });

            }
            else
            {
                var existing = appUser.InternalUserDocuments.FirstOrDefault(i => i.Id == image.id);
                if (existing == null)
                    throw new BadRequestException("Invalid InternalUser Documents, InternalUser Documents not found");

                // Only update if the image is passed for update
                if (fileId > 0)
                    existing.ImageId = fileId ?? 0;
                existing.DocumentIssuedFrom = image.documentIssuedFrom;
                existing.DocumentIssuedOn = image.documentIssuedOn;
                existing.DocumentName = image.documentName;
                existing.DocumentNumber = image.documentNumber;
                existing.DocumentType = image.documentType;

            }

            return (await _context.SaveChangesAsync()) > 0;

        }

        public async Task<bool> DeleteInternalUserDocuments(int appUserId, int id)
        {
            var internalUserDocuments = await _context.InternalUserDocuments
                .FirstOrDefaultAsync(s => s.Id == id && s.AppUserId == appUserId);

            if (internalUserDocuments == null)
                throw new NotFoundException("internalUser Documents is not found");

            internalUserDocuments.IsDeleted = true;
            return (await _context.SaveChangesAsync()) > 0;
        }

        #endregion

        public async Task<List<AppUserListDto>> GetAppUsers(AppUserFilter filter)
        {
            var appusers = _context.AppUsers
                            .Include(a => a.AppUserType)
                            .Include(a => a.AppUserRoles)
                            .Include(a => a.StoreUsers)
                            .IgnoreDeletedNavigationProperties()

                            .AsNoTracking()
                            .Where(a => !a.IsSystemAccount)
                            .AsQueryable();

            // initilize filter if filter is null
            if (filter == null)
                filter = new AppUserFilter();

            // Check if department for particular user type is requested or not, if not then 
            // return login user typed departments
            if (!(filter.UserTypeId).HasValue)
                filter.UserTypeId = (int)_currentUserService.AppUserType;

            // Onl

            if (filter != null)
            {
                if (filter.CreatedFrom.HasValue)
                {
                    if (!filter.CreatedTo.HasValue)
                        filter.CreatedTo = _dateTime.Now;
                    appusers = appusers.Where(a => a.CreatedOn.Date >= filter.CreatedFrom.Value.Date && a.CreatedOn.Date <= filter.CreatedTo.Value.Date);
                }

                if (filter.UserTypeId.HasValue)
                {
                    // Check for permission
                    if (_currentUserService.AppUserType == AppUserTypeEnum.Internal)
                        appusers = appusers.Where(a => a.AppUserTypeId == filter.UserTypeId);

                    else
                    {
                        throw new ForbiddenException();
                    }
                }

                if (!string.IsNullOrWhiteSpace(filter.Search))
                    appusers = appusers.Where(a => a.Username.Contains(filter.Search) || a.Email.Contains(filter.Search) || a.JobTitle.Contains(filter.Search) || a.FirstName.Contains(filter.Search) || a.MiddleName.Contains(filter.Search) || a.LastName.Contains(filter.Search) || a.PhoneNumber.Contains(filter.Search));

                if (filter.RoleId.HasValue)
                {
                    // TODO: Check for performance
                    appusers = appusers.Where(a => a.AppUserRoles.Select(r => r.RoleId).Where(r => r == filter.RoleId).Count() > 0);
                }

                if (filter.StoreId.HasValue)
                {
                    // TODO: Check for performance
                    appusers = appusers.Where(a => a.StoreUsers.Select(r => r.StoreId).Where(r => r == filter.StoreId).Count() > 0);
                }

                if (filter.IsActive.HasValue)
                    appusers = appusers.Where(a => a.Active == filter.IsActive);

            }
            return await appusers.Select(a => new AppUserListDto
            {
                AppUserType = a.AppUserType.Name,
                AppUserTypeId = a.AppUserTypeId,
                Email = a.Email,
                FirstName = a.FirstName,
                Id = a.Id,
                IsEmailVerified = a.IsEmailVerified,
                JobTitle = a.JobTitle,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                PhoneNumber = a.PhoneNumber,
                Username = a.Username,
                IsActive = a.Active
            }).ToListAsync();

        }

        public async Task<AppUserDto> GetAppUserById(int appUserId)
        {
            if (appUserId == 0)
                return null;

            var appUser = await _context.AppUsers
                           .Include(a => a.AppUserType)
                           .Include(a => a.AppUserRoles)
                               .ThenInclude(r => r.Role)
                           .Include(a => a.Address)
                               .ThenInclude(a => a.Country)
                           .Include(a => a.Address)
                               .ThenInclude(a => a.CountryProvince)
                           .Include(a => a.Address)
                               .ThenInclude(a => a.City)
                           .Include(a => a.Address)
                               .ThenInclude(a => a.Region)
                         .IgnoreDeletedNavigationProperties()

                           .AsNoTracking()
                           .Where(a => !a.IsSystemAccount)
                           .FirstOrDefaultAsync(u => u.Id == appUserId);
            if (appUser == null)
                throw new NotFoundException("AppUser not found", "Invalid appuser id");

            return new AppUserDto
            {

                AppUserType = appUser.AppUserType.Name,
                AppUserTypeId = appUser.AppUserTypeId,
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                Id = appUser.Id,
                IsEmailVerified = appUser.IsEmailVerified,
                JobTitle = appUser.JobTitle,
                LastName = appUser.LastName,
                MiddleName = appUser.MiddleName,
                PhoneNumber = appUser.PhoneNumber,
                Username = appUser.Username,
                IsActive = appUser.Active,
                AddressId = appUser.AddressId,
                Address = appUser.Address == null ? null : new Addresses.Models.Dtos.AddressDto
                {
                    Id = appUser.Address.Id,
                    Email = appUser.Address.Email,
                    CountryId = appUser.Address.CountryId,
                    CountryProvinceId = appUser.Address.CountryProvinceId,
                    City = appUser.Address.City?.Name,
                    CityId = appUser.Address.CityId,
                    RegionId = appUser.Address.RegionId,
                    Country = appUser.Address.Country?.Name,
                    Region = appUser.Address.Region?.Name,
                    CountryProvince = appUser.Address.CountryProvince?.Name,
                    Address1 = appUser.Address.Address1,
                    Address2 = appUser.Address.Address2,
                    ZipPostalCode = appUser.Address.ZipPostalCode,
                    PhoneNumber = appUser.Address.PhoneNumber,
                    SecondaryPhoneNumber = appUser.Address.SecondaryPhoneNumber,
                    FaxNumber = appUser.Address.FaxNumber,
                    MapLink = appUser.Address.MapLink
                },
                AdminComment = appUser.AdminComment,
                AppUserRoles = appUser.AppUserRoles.Select(r => new RoleDto
                {
                    Name = r.Role.Name,
                    Description = r.Role.Description,
                    DisplayOrder = r.Role.DisplayOrder,
                    Id = r.RoleId
                }).ToList(),
                CannotLoginUntilDate = appUser.CannotLoginUntilDate,
                FailedLoginAttempts = appUser.FailedLoginAttempts,
                LimitedToStores = appUser.LimitedToStores,
                RegisteredInStore = appUser.RegisteredInStore?.Name,
                RegisteredInStoreId = appUser.RegisteredInStoreId,
                RequireReLogin = appUser.RequireReLogin,
            };
        }

        public async Task<bool> AddRemoveAppUserRole(AppUserRoleCommand changeUserRole, bool isRoleAdd = true)
        {

            var user = await _context.AppUsers
                .Include(u => u.AppUserRoles)
                .FirstOrDefaultAsync(u => u.Id == changeUserRole.AppUserId);

            if (user == null)
                throw new BadRequestException("Invalid User");

            var role = await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == changeUserRole.RoleId);

            if (role == null)
                throw new BadRequestException("Invalid Role");

            // Check if role is system role
            if (role.IsSystemRole)
                throw new BadRequestException("Invalid Role");

            // Get appUser role of the user
            var appUserRole = user.AppUserRoles.FirstOrDefault(r => r.RoleId == changeUserRole.RoleId);

            // CHeck if the role is add or delete
            if (isRoleAdd)
            {
                if (appUserRole != null)
                    throw new BadRequestException("Role already exists for the user.");

                user.AppUserRoles.Add(new AppUserRole
                {
                    AppUserId = changeUserRole.AppUserId,
                    RoleId = changeUserRole.RoleId,
                    IsDeleted = false,
                });
            }
            // Delete role
            else
            {
                if (appUserRole != null)
                {
                    appUserRole.IsDeleted = true;
                }
            }
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> ChangeAppUserPassword(ChangePasswordCommand changePassword)
        {
            //if (changePassword.AppUserTypeId <= 0)
            //    changePassword.AppUserTypeId = (int)AppUserTypeEnum.Customer;

            var user = await _context.AppUsers
                .Include(u => u.AppUserPasswords)
                .FirstOrDefaultAsync(f => f.Username == changePassword.Username.Trim() && f.AppUserTypeId == changePassword.AppUserTypeId);

            if (user == null)
                throw new BadRequestException("Invalid Username or Password");

            var userPassword = user.AppUserPasswords.FirstOrDefault(p => p.IsCurrent);
            userPassword.IsCurrent = false;

            // Check for valid password
            if (!PasswordHelper.VerifyPasswordHash(changePassword.OldPassword, userPassword.PasswordHash, userPassword.PasswordSalt))
                throw new BadRequestException("Invalid Username or Password");

            // Hash the passowrd
            byte[] passwordHash, passwordSalt;
            PasswordHelper.CreatePasswordHash(changePassword.NewPassword, out passwordHash, out passwordSalt);

            user.AppUserPasswords.Add(new AppUserPassword
            {
                CreatedOn = _dateTime.Now,
                EnablePasswordLifetime = changePassword.EnablePasswordLifetime,
                IsCurrent = true,
                PasswordExpiredOn = changePassword.PasswordExpiredOn,
                RequiredPasswordChange = changePassword.RequiredPasswordChange,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            });
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> ForgotPassword(ForgotPasswordCommand forgotPassword)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(f =>
            (f.Username == forgotPassword.Username || f.PhoneNumber == forgotPassword.Username) && f.AppUserTypeId == forgotPassword.AppUserTypeId);
            if (user == null)
                throw new NotFoundException("User not found");
            //var randomText = PasswordHelper.GetRandomPassword();
            var randomText = _codeService.Get8Digits().ToString();

            var newOtpCode = new AppUserOtpCode
            {
                Code = randomText,
                IsCodeUsed = false,
                AppUserId = user.Id,
                CodeGeneratedOn = _dateTime.Now,
                CodeExpireOn = _dateTime.Now.AddMinutes(_appSettings.ResetPasswordExpiryMinutes),

            };
            _context.AppUserOtpCodes.Add(newOtpCode);
            var res = await _context.SaveChangesAsync();

            //SMS Send
            if (forgotPassword.SendToPhonenumber == true)
            {
                if (_appSettings.SendSMS == true)
                {
                    try
                    {
                        var text = "Dear " + user.FirstName + " , please use " + randomText + ", to reset your Hattiya password.";

                        _sMSSenderService.SendSMS(_appSettings.SendSMSFrom, _appSettings.AccessSMSToken, user.PhoneNumber, text);

                    }
                    catch (Exception)
                    {

                    }
                }

            }
            //email.
            if (forgotPassword.SendToEmail == true)
            {
                try
                {
                    // Send email
                    //if (res > 0)
                    _emailService.ForgotPassword(new ForgotPasswordEmailViewModel(user.Username, randomText, user.Email));

                }
                catch (Exception)
                {
                }
            }
            ////throw new BadRequestException($"For debugging process only, Code: {randomText}");

            return res > 0;
        }

        public async Task<bool> ResendVerificationCode(ResendVerificationCode username)
        {
            var user = await _context.AppUsers
                .FirstOrDefaultAsync(f => (f.Username == username.Username || f.PhoneNumber == username.Username) && f.AppUserTypeId == username.AppUserTypeId);
            if (user == null)
                throw new NotFoundException("User not found");
            //var randomText = PasswordHelper.GetRandomPassword();
            var randomText = _codeService.Get8Digits().ToString();

            // Expire all the perivious code 
            foreach (var code in _context.AppUserOtpCodes.Where(a => a.AppUserId == user.Id && !a.IsCodeUsed))
            {
                code.CodeExpireOn = _dateTime.Now;
            }

            var newOtpCode = new AppUserOtpCode
            {
                Code = randomText,
                IsCodeUsed = false,
                AppUserId = user.Id,
                CodeGeneratedOn = _dateTime.Now,
                CodeExpireOn = _dateTime.Now.AddMinutes(_appSettings.ResetPasswordExpiryMinutes),

            };
            _context.AppUserOtpCodes.Add(newOtpCode);
            var res = await _context.SaveChangesAsync();

            try
            {

                // Send email
                if (res > 0)

                    if (username.SendToEmail == true)
                    {
                        _emailService.ResendVerificationCode(new ResendVerificationCodeEmailViewModel(user.Username, randomText, user.Email));

                    }
                if (username.SendToPhonenumber == true)
                {
                    if (user.PhoneNumber != null)
                    {
                        if (_appSettings.SendSMS == true)
                        {
                            try
                            {
                                var text = "Dear " + user.FirstName + " , thank you for registration and welcome to Hattiya. Please use " + randomText + " as your account verification code.";

                                _sMSSenderService.SendSMS(_appSettings.SendSMSFrom, _appSettings.AccessSMSToken, user.PhoneNumber, text);

                            }
                            catch
                            {

                            }
                        }
                    }
                }
                //SMS Send



            }
            catch (Exception)
            {
                if (username.SendToEmail == true)
                {
                    _emailService.ResendVerificationCode(new ResendVerificationCodeEmailViewModel(user.Username, randomText, user.Email));

                }
                if (username.SendToPhonenumber == true)
                {                //SMS Send
                    if (user.PhoneNumber != null)
                    {
                        if (_appSettings.SendSMS == true)
                        {
                            try
                            {
                                var text = "Dear " + user.FirstName + " , thank you for registration and welcome to Hattiya. Please use " + randomText + " as your account verification code.";

                                _sMSSenderService.SendSMS(_appSettings.SendSMSFrom, _appSettings.AccessSMSToken, user.PhoneNumber, text);

                            }
                            catch
                            {

                            }
                        }
                    }
                }
            }
            return res > 0;
        }
        public async Task<bool> ResetPassword(ResetPasswordCommand resetPassword)
        {
            //if (resetPassword.AppUserTypeId <= 0)
            //    resetPassword.AppUserTypeId = (int)AppUserTypeEnum.Customer;

            var user = await _context.AppUsers
                .Include(a => a.AppUserPasswords)
                .OrderByDescending(o => o.Id)

                .FirstOrDefaultAsync(f => (f.Username == resetPassword.Username || f.PhoneNumber == resetPassword.Username) && f.AppUserTypeId == resetPassword.AppUserTypeId);
            if (user == null)
                throw new NotFoundException("Invalid username or code");

            var forgotUser = await _context.AppUserOtpCodes
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync
                (
                    f => f.AppUserId == user.Id
                     && !f.IsCodeUsed
                    && f.Code == resetPassword.Code
                );
            if (forgotUser == null)
                throw new NotFoundException("Invalid username or code");

            // Check for expiry
            if (forgotUser.CodeExpireOn < _dateTime.Now)
                throw new BadRequestException("Recovery code expired");

            forgotUser.IsCodeUsed = true;
            forgotUser.CodeUsedOn = _dateTime.Now;

            // Change current password flag
            var currentPassword = user.AppUserPasswords.FirstOrDefault(a => a.IsCurrent);
            if (currentPassword != null)
                currentPassword.IsCurrent = false;
            else
                currentPassword = new AppUserPassword
                {
                    EnablePasswordLifetime = _appSettings.EnablePasswordLifetime,
                    PasswordExpiredOn = _dateTime.Now.AddDays(_appSettings.DefaultPasswordExpiryDays)
                };

            // Change password
            // Hash the passowrd
            byte[] passwordHash, passwordSalt;
            PasswordHelper.CreatePasswordHash(resetPassword.NewPassword, out passwordHash, out passwordSalt);

            user.AppUserPasswords.Add(new AppUserPassword
            {
                AppUserId = user.Id,
                CreatedOn = _dateTime.Now,
                EnablePasswordLifetime = currentPassword.EnablePasswordLifetime,
                IsCurrent = true,
                PasswordExpiredOn = currentPassword.PasswordExpiredOn,
                RequiredPasswordChange = false,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });


            var isSuccess = (await _context.SaveChangesAsync()) > 0;

            // Send email
            if (isSuccess)
                if (resetPassword.SendToEmail == true)
                {
                    _emailService.ResetPassword(new ResetPasswordEmailViewModel(user.Username, user.Email));

                }
            if (resetPassword.SendToPhonenumber == true)
            {                //SMS Send
                if (user.PhoneNumber != null)
                {
                    if (_appSettings.SendSMS == true)
                    {
                        try
                        {
                            var text = "Dear " + user.FirstName + " , Your password has been successfully reset for the account.";

                            _sMSSenderService.SendSMS(_appSettings.SendSMSFrom, _appSettings.AccessSMSToken, user.PhoneNumber, text);

                        }
                        catch
                        {

                        }
                    }
                }
            }

            return isSuccess;
        }
        public async Task<ValidUserDto> ValidateLogin(Login loginCommand)
        {
            // Get the user
            var user = await _context.AppUsers
                .Include(a => a.AppUserPasswords)
                .Include(a => a.AppUserLoginHistories)
                .Include(a => a.AppUserRoles)
                .Include(a => a.StoreUsers)
                .FirstOrDefaultAsync(u => u.Username == loginCommand.UserName &&
                (u.AppUserTypeId == (int)AppUserTypeEnum.Internal));

            //_eventPublisher.Publish (new AppUserLoggedinEvent(user));
            //_eventPublisher.Publish(new AppUserRegisteredEvent (user));
            //_eventPublisher.Publish(new AppUserLoggedOutEvent(user));
            //_eventPublisher.EntityInserted(user);
            //_eventPublisher.EntityUpdated(user);

            if (user == null)
                return new ValidUserDto
                {
                    IsLoginValid = false,
                    Remarks = "Invalid username or password."
                };


            //check whether a user is locked out
            if (user.CannotLoginUntilDate.HasValue && user.CannotLoginUntilDate.Value > _dateTime.Now)
                return new ValidUserDto
                {
                    IsLoginValid = false,
                    Remarks = $"Account is Lockout. Cannot login until {user.CannotLoginUntilDate.Value}"
                };

            // Get the password
            var userPassword = user.AppUserPasswords.FirstOrDefault(p => p.IsCurrent);

            if (userPassword == null)
                return new ValidUserDto
                {
                    IsLoginValid = false,
                    Remarks = "User does not have password."
                };

            // Verify password
            if (!PasswordHelper.VerifyPasswordHash(loginCommand.Password, userPassword.PasswordHash, userPassword.PasswordSalt))
            {
                //wrong password
                user.FailedLoginAttempts++;
                if (_appSettings.FailedPasswordAllowedAttempts > 0 &&
                    user.FailedLoginAttempts >= _appSettings.FailedPasswordAllowedAttempts)
                {
                    //lock out
                    user.CannotLoginUntilDate = _dateTime.Now.AddMinutes(_appSettings.FailedPasswordLockoutMinutes);
                    //reset the counter
                    user.FailedLoginAttempts = 0;
                }

                await _context.SaveChangesAsync();

                return new ValidUserDto
                {
                    IsLoginValid = false,
                    Remarks = $"Invalid username or password."
                };
            }

            // Check if the password is expired
            if (userPassword.EnablePasswordLifetime && userPassword.PasswordExpiredOn < _dateTime.Now)
                return new ValidUserDto
                {
                    IsLoginValid = false,
                    Remarks = "Password is expired",
                    RequiredPasswordChange = true,

                };

            //update login details
            user.FailedLoginAttempts = 0;
            user.CannotLoginUntilDate = null;
            user.RequireReLogin = false;

            // Add to login history
            if (_appSettings.SaveLoginHistory)
                user.AppUserLoginHistories.Add(new AppUserLoginHistory
                {
                    AppUserId = user.Id,
                    LoginDateTime = _dateTime.Now,
                    IpAddress = _currentUserService.UserIpAddress
                });

            await _context.SaveChangesAsync();

            // Get user permissions from user roles
            var permissions = await _context.RolePermissions
                .Where(r => (user.AppUserRoles.Select(ar => ar.RoleId)).Contains(r.RoleId))
                .Select(r => r.PermissionId)
                .Distinct()
                .ToListAsync()
                ;



            // Claims of the user
            var userClaims = new UsersClaims(user.Id, user.Username, user.AppUserTypeId, permissions);


            return new ValidUserDto
            {
                IsLoginValid = true,
                Remarks = null,
                RequiredPasswordChange = userPassword.RequiredPasswordChange,
                Token = _jwtService.GenerateToken(userClaims),
                AppUserTypeId = user.AppUserTypeId,
                FullName = $"{user.FirstName } {user.LastName}",
            };

        }

        public List<EnumValue> GetAppUserType()
        {
            var userTypes = EnumHelper.GetEnumValues<AppUserTypeEnum>();
            return userTypes;

        }

    }
}