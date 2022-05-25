using PreSchool.Application.Exceptions;
using PreSchool.Application.HelperClasses;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Services.AppConfigurations;
using PreSchool.Application.Services.Files;
using PreSchool.Data;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using PreSchool.Application.Services.Students.Models.Commands;
using PreSchool.Data.Entities.Students;
using PreSchool.Application.Services.Students.Models.Dtos;
using PreSchool.Application.Services.Students.Models.Queries;

namespace PreSchool.Application.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IFileService _fileService;
        private readonly IAppFeatureService _appFeatureService;

        public StudentService(
            IApplicationDbContext context,
            IDateTime dateTime,
            ICurrentUserService currentUserService,
            IFileService fileService,
            IAppFeatureService appFeatureService
            )
        {
            _context = context;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _fileService = fileService;
            _appFeatureService = appFeatureService;
        }


        #region StudentEnquiry.

        public async Task<int> InsertUpdateStudentEnquiry(InsertUpdateStudentEnquiry studentCmd)
        {

            if (studentCmd.Id == 0)
            {
                var newStudentEnquiry = new StudentEnquiry
                {
                    AdmissionTypeId = studentCmd.AdmissionTypeId,
                    Age = studentCmd.Age,
                    DateOfBirthAD = studentCmd.DateOfBirthAD,
                    DateOfBirthBS = studentCmd.DateOfBirthBS,
                    FatherEmail = studentCmd.FatherEmail,
                    FatherName = studentCmd.FatherName,
                    FatherOccupation = studentCmd.FatherOccupation,
                    FatherOrganization = studentCmd.FatherOrganization,
                    FatherPhone = studentCmd.FatherPhone,
                    MotherEmail = studentCmd.MotherEmail,
                    MotherName = studentCmd.MotherName,
                    MotherOccupation = studentCmd.MotherOccupation,
                    MotherOrganization = studentCmd.MotherOrganization,
                    MotherPhone = studentCmd.MotherPhone,
                    FirstName = studentCmd.FirstName,
                    MiddleName = studentCmd.MiddleName,
                    LastName = studentCmd.LastName,
                    GenderId = studentCmd.GenderId,
                    KnownThroughId = studentCmd.KnownThroughId,
                    Remark = studentCmd.Remark,

                };
                // Address
                if (studentCmd.Address != null)
                {
                    if (newStudentEnquiry.Address == null)
                        newStudentEnquiry.Address = new Data.Entities.Address();

                    newStudentEnquiry.Address.Email = studentCmd.Address.Email;
                    newStudentEnquiry.Address.CountryId = studentCmd.Address.CountryId == 0 ? null : studentCmd.Address.CountryId;
                    newStudentEnquiry.Address.CountryProvinceId = studentCmd.Address.CountryProvinceId == 0 ? null : studentCmd.Address.CountryProvinceId;
                    newStudentEnquiry.Address.CityId = studentCmd.Address.CityId;
                    newStudentEnquiry.Address.RegionId = studentCmd.Address.RegionId;
                    newStudentEnquiry.Address.Address1 = studentCmd.Address.Address1;
                    newStudentEnquiry.Address.Address2 = studentCmd.Address.Address2;
                    newStudentEnquiry.Address.ZipPostalCode = studentCmd.Address.ZipPostalCode;
                    newStudentEnquiry.Address.PhoneNumber = studentCmd.Address.PhoneNumber;
                    newStudentEnquiry.Address.SecondaryPhoneNumber = studentCmd.Address.SecondaryPhoneNumber;
                    newStudentEnquiry.Address.FaxNumber = studentCmd.Address.FaxNumber;
                    newStudentEnquiry.Address.MapLink = studentCmd.Address.MapLink; ;
                }
                _context.StudentEnquiries.Add(newStudentEnquiry);
                await _context.SaveChangesAsync();
                return newStudentEnquiry.Id;
            }
            else
            {
                var existing = await _context.StudentEnquiries
                                      .Include(s => s.Address)
                                      .FirstOrDefaultAsync(i => i.Id == studentCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student enqueries not found");

                existing.AdmissionTypeId = studentCmd.AdmissionTypeId;
                existing.Age = studentCmd.Age;
                existing.DateOfBirthAD = studentCmd.DateOfBirthAD;
                existing.DateOfBirthBS = studentCmd.DateOfBirthBS;
                existing.FatherEmail = studentCmd.FatherEmail;
                existing.FatherName = studentCmd.FatherName;
                existing.FatherOccupation = studentCmd.FatherOccupation;
                existing.FatherOrganization = studentCmd.FatherOrganization;
                existing.FatherPhone = studentCmd.FatherPhone;
                existing.MotherEmail = studentCmd.MotherEmail;
                existing.MotherName = studentCmd.MotherName;
                existing.MotherOccupation = studentCmd.MotherOccupation;
                existing.MotherOrganization = studentCmd.MotherOrganization;
                existing.MotherPhone = studentCmd.MotherPhone;
                existing.FirstName = studentCmd.FirstName;
                existing.MiddleName = studentCmd.MiddleName;
                existing.LastName = studentCmd.LastName;
                existing.GenderId = studentCmd.GenderId;
                existing.KnownThroughId = studentCmd.KnownThroughId;
                existing.Remark = studentCmd.Remark;

                // Address
                if (studentCmd.Address != null)
                {
                    if (existing.Address == null)
                        existing.Address = new Data.Entities.Address();

                    existing.Address.Email = studentCmd.Address.Email;
                    existing.Address.CountryId = studentCmd.Address.CountryId == 0 ? null : studentCmd.Address.CountryId;
                    existing.Address.CountryProvinceId = studentCmd.Address.CountryProvinceId == 0 ? null : studentCmd.Address.CountryProvinceId;
                    existing.Address.CityId = studentCmd.Address.CityId;
                    existing.Address.RegionId = studentCmd.Address.RegionId;
                    existing.Address.Address1 = studentCmd.Address.Address1;
                    existing.Address.Address2 = studentCmd.Address.Address2;
                    existing.Address.ZipPostalCode = studentCmd.Address.ZipPostalCode;
                    existing.Address.PhoneNumber = studentCmd.Address.PhoneNumber;
                    existing.Address.SecondaryPhoneNumber = studentCmd.Address.SecondaryPhoneNumber;
                    existing.Address.FaxNumber = studentCmd.Address.FaxNumber;
                    existing.Address.MapLink = studentCmd.Address.MapLink;
                }
                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<PagedResult<StudentEnquiryListDto>> StudentEnquiryList(StudentEnquiryFilter filter)
        {

            //if (!_currentUserService.HavePermission(Permissions.ViewCustomerOrder))
            //    throw new ForbiddenException();

            var studentEnquiries = _context.StudentEnquiries
                    .IgnoreDeletedNavigationProperties()
                .AsNoTracking()
                .AsQueryable();

            if (filter != null)
            {
                if (filter.GenderId.HasValue)
                    studentEnquiries = studentEnquiries.Where(o => o.GenderId == filter.GenderId);


            }

            return await studentEnquiries.Select(o => new StudentEnquiryListDto
            {
                Id = o.Id,
                ImageId = o.ImageId,
                Image = o.ImageId > 0 ? o.Image.Path : null,
                AdmissionTypeId = o.AdmissionTypeId,
                AdmissionType = o.AdmissionTypeId.ToNameString(),
                Age = o.Age,
                DateOfBirthAD = o.DateOfBirthAD,
                DateOfBirthBS = o.DateOfBirthBS,
                FatherEmail = o.FatherEmail,
                FatherName = o.FatherName,
                FatherOccupation = o.FatherOccupation,
                FatherOrganization = o.FatherOrganization,
                FatherPhone = o.FatherPhone,
                MotherEmail = o.MotherEmail,
                MotherName = o.MotherName,
                MotherOccupation = o.MotherOccupation,
                MotherOrganization = o.MotherOrganization,
                MotherPhone = o.MotherPhone,
                FirstName = o.FirstName,
                MiddleName = o.MiddleName,
                LastName = o.LastName,
                GenderId = o.GenderId,
                Gender = o.GenderId.ToNameString(),

                KnownThroughId = o.KnownThroughId,
                KnownThrough = o.KnownThroughId.ToNameString(),

                Remark = o.Remark,

            }).OrderBy(filter.SortBy).GetPagedAsync(filter.PageNumber, filter.PageSize);

        }

        public async Task<StudentEnquiryDto> GetStudentEnquiryById(int id)
        {
            var studentEnquiry = await _context.StudentEnquiries
                 .Include(n => n.Address)
                .ThenInclude(a => a.City)
                .Include(n => n.Address)
                .ThenInclude(a => a.CountryProvince)
                  .Include(n => n.Address)
                .ThenInclude(a => a.Country)
                   .Include(n => n.Address)
                .ThenInclude(a => a.Region)

                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (studentEnquiry == null)
                throw new NotFoundException("student Enquiry not found");

            return new StudentEnquiryDto
            {
                Id = studentEnquiry.Id,
                ImageId = studentEnquiry.ImageId,
                Image = studentEnquiry.ImageId > 0 ? studentEnquiry.Image.Path : null,
                AdmissionTypeId = studentEnquiry.AdmissionTypeId,
                AdmissionType = studentEnquiry.AdmissionTypeId.ToNameString(),
                Age = studentEnquiry.Age,
                DateOfBirthAD = studentEnquiry.DateOfBirthAD,
                DateOfBirthBS = studentEnquiry.DateOfBirthBS,
                FatherEmail = studentEnquiry.FatherEmail,
                FatherName = studentEnquiry.FatherName,
                FatherOccupation = studentEnquiry.FatherOccupation,
                FatherOrganization = studentEnquiry.FatherOrganization,
                FatherPhone = studentEnquiry.FatherPhone,
                MotherEmail = studentEnquiry.MotherEmail,
                MotherName = studentEnquiry.MotherName,
                MotherOccupation = studentEnquiry.MotherOccupation,
                MotherOrganization = studentEnquiry.MotherOrganization,
                MotherPhone = studentEnquiry.MotherPhone,
                FirstName = studentEnquiry.FirstName,
                MiddleName = studentEnquiry.MiddleName,
                LastName = studentEnquiry.LastName,
                GenderId = studentEnquiry.GenderId,
                Gender = studentEnquiry.GenderId.ToNameString(),

                KnownThroughId = studentEnquiry.KnownThroughId,
                KnownThrough = studentEnquiry.KnownThroughId.ToNameString(),

                Address = studentEnquiry.Address == null ? null : new Addresses.Models.Dtos.AddressDto
                {
                    Id = studentEnquiry.Address.Id,
                    Email = studentEnquiry.Address.Email,
                    CountryId = studentEnquiry.Address.CountryId,
                    CountryProvinceId = studentEnquiry.Address.CountryProvinceId,
                    City = studentEnquiry.Address.City?.Name,
                    CityId = studentEnquiry.Address.CityId,
                    RegionId = studentEnquiry.Address.RegionId,
                    Country = studentEnquiry.Address.Country?.Name,
                    Region = studentEnquiry.Address.Region?.Name,
                    CountryProvince = studentEnquiry.Address.CountryProvince?.Name,
                    Address1 = studentEnquiry.Address.Address1,
                    Address2 = studentEnquiry.Address.Address2,
                    ZipPostalCode = studentEnquiry.Address.ZipPostalCode,
                    PhoneNumber = studentEnquiry.Address.PhoneNumber,
                    SecondaryPhoneNumber = studentEnquiry.Address.SecondaryPhoneNumber,
                    FaxNumber = studentEnquiry.Address.FaxNumber,
                    MapLink = studentEnquiry.Address.MapLink
                },

            };
        }
        public async Task<bool> DeleteStudentEnquiry(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var studentEnquiry = await _context.StudentEnquiries.FindAsync(id);
            if (studentEnquiry == null)
                throw new BadRequestException("Invalid student Enquiry.");

            studentEnquiry.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> InsertStudentEnquiryImage(InsertStudentEnquiryImage image)
        {
            var studentEnquiry = await _context.StudentEnquiries.FirstOrDefaultAsync(v => v.Id == image.studentEnquiryId);
            if (studentEnquiry == null)
                throw new BadRequestException("student Enquiry not found");


            // Logo
            if (image.image != null)
            {

                studentEnquiry.ImageId = await _fileService.InsertFile(new Files.Models.InsertFileCommand
                {
                    EntityName = nameof(StudentEnquiry),
                    File = image.image
                });

            }

            return (await _context.SaveChangesAsync()) > 0;

        }
        #endregion

        #region Student Registration.

        public async Task<int> InsertUpdateStudentRegistration(InsertUpdateStudentRegistration studentCmd)
        {

            if (studentCmd.Id == 0)
            {
                var newStudentRegistration = new StudentRegistration
                {
                    BatchId = studentCmd.BatchId,
                    DateOfBirthAD = studentCmd.DateOfBirthAD,
                    DateOfBirthBS = studentCmd.DateOfBirthBS,
                    FatherEmail = studentCmd.FatherEmail,
                    FatherName = studentCmd.FatherName,
                    FatherOccupation = studentCmd.FatherOccupation,
                    FatherOrganization = studentCmd.FatherOrganization,
                    FatherPhone = studentCmd.FatherPhone,
                    MotherEmail = studentCmd.MotherEmail,
                    MotherName = studentCmd.MotherName,
                    MotherOccupation = studentCmd.MotherOccupation,
                    MotherOrganization = studentCmd.MotherOrganization,
                    MotherPhone = studentCmd.MotherPhone,
                    FirstName = studentCmd.FirstName,
                    MiddleName = studentCmd.MiddleName,
                    LastName = studentCmd.LastName,
                    GenderId = studentCmd.GenderId,
                    BloodGroup = studentCmd.BloodGroup,
                    BirthCertificateNo = studentCmd.BirthCertificateNo,
                    DateOfIssueAD = studentCmd.DateOfIssueAD,
                    DateOfIssueBS = studentCmd.DateOfIssueBS,
                    PlaceOfIssue = studentCmd.PlaceOfIssue,
                    IssueCityId = studentCmd.IssueCityId,
                    PassportNo = studentCmd.PassportNo,
                    ValidUpToAD = studentCmd.ValidUpToAD,
                    VisaCategory = studentCmd.VisaCategory,
                    DateOfEnrollmentAD = studentCmd.DateOfEnrollmentAD,
                    GradeId = studentCmd.GradeId,
                    Section = studentCmd.Section,
                    RegistrationNo = studentCmd.RegistrationNo,
                    RollNo = studentCmd.RollNo,
                    StudentTypeId = studentCmd.StudentTypeId,
                    GuardianName = studentCmd.GuardianName,
                    GuardianAddress = studentCmd.GuardianAddress,
                    GuardianEmail = studentCmd.GuardianEmail,
                    GuardianPhone = studentCmd.GuardianPhone,
                    RelationWithGuardian = studentCmd.RelationWithGuardian,
                    StudentEnquiryId = studentCmd.StudentEnquiryId,


                };
                // Address
                if (studentCmd.Address != null)
                {
                    if (newStudentRegistration.Address == null)
                        newStudentRegistration.Address = new Data.Entities.Address();

                    newStudentRegistration.Address.Email = studentCmd.Address.Email;
                    newStudentRegistration.Address.CountryId = studentCmd.Address.CountryId == 0 ? null : studentCmd.Address.CountryId;
                    newStudentRegistration.Address.CountryProvinceId = studentCmd.Address.CountryProvinceId == 0 ? null : studentCmd.Address.CountryProvinceId;
                    newStudentRegistration.Address.CityId = studentCmd.Address.CityId;
                    newStudentRegistration.Address.RegionId = studentCmd.Address.RegionId;
                    newStudentRegistration.Address.Address1 = studentCmd.Address.Address1;
                    newStudentRegistration.Address.Address2 = studentCmd.Address.Address2;
                    newStudentRegistration.Address.ZipPostalCode = studentCmd.Address.ZipPostalCode;
                    newStudentRegistration.Address.PhoneNumber = studentCmd.Address.PhoneNumber;
                    newStudentRegistration.Address.SecondaryPhoneNumber = studentCmd.Address.SecondaryPhoneNumber;
                    newStudentRegistration.Address.FaxNumber = studentCmd.Address.FaxNumber;
                    newStudentRegistration.Address.MapLink = studentCmd.Address.MapLink; ;
                }
                _context.StudentRegistrations.Add(newStudentRegistration);
                await _context.SaveChangesAsync();
                return newStudentRegistration.Id;
            }
            else
            {
                var existing = await _context.StudentRegistrations
                                      .Include(s => s.Address)
                                      .FirstOrDefaultAsync(i => i.Id == studentCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student enqueries not found");

                existing.BatchId = studentCmd.BatchId;
                existing.DateOfBirthAD = studentCmd.DateOfBirthAD;
                existing.DateOfBirthBS = studentCmd.DateOfBirthBS;
                existing.FatherEmail = studentCmd.FatherEmail;
                existing.FatherName = studentCmd.FatherName;
                existing.FatherOccupation = studentCmd.FatherOccupation;
                existing.FatherOrganization = studentCmd.FatherOrganization;
                existing.FatherPhone = studentCmd.FatherPhone;
                existing.MotherEmail = studentCmd.MotherEmail;
                existing.MotherName = studentCmd.MotherName;
                existing.MotherOccupation = studentCmd.MotherOccupation;
                existing.MotherOrganization = studentCmd.MotherOrganization;
                existing.MotherPhone = studentCmd.MotherPhone;
                existing.FirstName = studentCmd.FirstName;
                existing.MiddleName = studentCmd.MiddleName;
                existing.LastName = studentCmd.LastName;
                existing.GenderId = studentCmd.GenderId;
                existing.BloodGroup = studentCmd.BloodGroup;
                existing.BirthCertificateNo = studentCmd.BirthCertificateNo;
                existing.DateOfIssueAD = studentCmd.DateOfIssueAD;
                existing.DateOfIssueBS = studentCmd.DateOfIssueBS;
                existing.PlaceOfIssue = studentCmd.PlaceOfIssue;
                existing.IssueCityId = studentCmd.IssueCityId;
                existing.PassportNo = studentCmd.PassportNo;
                existing.ValidUpToAD = studentCmd.ValidUpToAD;
                existing.VisaCategory = studentCmd.VisaCategory;
                existing.DateOfEnrollmentAD = studentCmd.DateOfEnrollmentAD;
                existing.GradeId = studentCmd.GradeId;
                existing.Section = studentCmd.Section;
                existing.RegistrationNo = studentCmd.RegistrationNo;
                existing.RollNo = studentCmd.RollNo;
                existing.StudentTypeId = studentCmd.StudentTypeId;
                existing.GuardianName = studentCmd.GuardianName;
                existing.GuardianAddress = studentCmd.GuardianAddress;
                existing.GuardianEmail = studentCmd.GuardianEmail;
                existing.GuardianPhone = studentCmd.GuardianPhone;
                existing.RelationWithGuardian = studentCmd.RelationWithGuardian;

                // Address
                if (studentCmd.Address != null)
                {
                    if (existing.Address == null)
                        existing.Address = new Data.Entities.Address();

                    existing.Address.Email = studentCmd.Address.Email;
                    existing.Address.CountryId = studentCmd.Address.CountryId == 0 ? null : studentCmd.Address.CountryId;
                    existing.Address.CountryProvinceId = studentCmd.Address.CountryProvinceId == 0 ? null : studentCmd.Address.CountryProvinceId;
                    existing.Address.CityId = studentCmd.Address.CityId;
                    existing.Address.RegionId = studentCmd.Address.RegionId;
                    existing.Address.Address1 = studentCmd.Address.Address1;
                    existing.Address.Address2 = studentCmd.Address.Address2;
                    existing.Address.ZipPostalCode = studentCmd.Address.ZipPostalCode;
                    existing.Address.PhoneNumber = studentCmd.Address.PhoneNumber;
                    existing.Address.SecondaryPhoneNumber = studentCmd.Address.SecondaryPhoneNumber;
                    existing.Address.FaxNumber = studentCmd.Address.FaxNumber;
                    existing.Address.MapLink = studentCmd.Address.MapLink;
                }
                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<PagedResult<StudentRegistrationListDto>> StudentRegistrationList(StudentRegistrationFilter filter)
        {

            //if (!_currentUserService.HavePermission(Permissions.ViewCustomerOrder))
            //    throw new ForbiddenException();

            var studentRegistrations = _context.StudentRegistrations
                  .Include(s => s.Batch)
                  .Include(s => s.IssueCity)
                  .Include(s => s.Grade)
                    .IgnoreDeletedNavigationProperties()
                .AsNoTracking()
                .AsQueryable();

            if (filter != null)
            {
                if (filter.EnquiredStudent.HasValue)
                    studentRegistrations = studentRegistrations.Where(o => o.StudentEnquiryId > 0);


            }

            return await studentRegistrations.Select(o => new StudentRegistrationListDto
            {
                Id = o.Id,
                ImageId = o.ImageId,
                Image = o.ImageId > 0 ? o.Image.Path : null,
                BatchId = o.BatchId,
                Batch = o.Batch.Name,
                DateOfBirthAD = o.DateOfBirthAD,
                DateOfBirthBS = o.DateOfBirthBS,
                FatherEmail = o.FatherEmail,
                FatherName = o.FatherName,
                FatherOccupation = o.FatherOccupation,
                FatherOrganization = o.FatherOrganization,
                FatherPhone = o.FatherPhone,
                MotherEmail = o.MotherEmail,
                MotherName = o.MotherName,
                MotherOccupation = o.MotherOccupation,
                MotherOrganization = o.MotherOrganization,
                MotherPhone = o.MotherPhone,
                FirstName = o.FirstName,
                MiddleName = o.MiddleName,
                LastName = o.LastName,
                GenderId = o.GenderId,
                Gender = o.GenderId.ToNameString(),
                BloodGroup = o.BloodGroup,
                BirthCertificateNo = o.BirthCertificateNo,
                DateOfIssueAD = o.DateOfIssueAD,
                DateOfIssueBS = o.DateOfIssueBS,
                PlaceOfIssue = o.PlaceOfIssue,
                IssueCityId = o.IssueCityId,
                CityName = o.IssueCity.Name,
                PassportNo = o.PassportNo,
                ValidUpToAD = o.ValidUpToAD,
                VisaCategory = o.VisaCategory,
                DateOfEnrollmentAD = o.DateOfEnrollmentAD,
                GradeId = o.GradeId,
                Grade = o.Grade.Name,
                Section = o.Section,
                RegistrationNo = o.RegistrationNo,
                RollNo = o.RollNo,
                StudentTypeId = o.StudentTypeId,
                StudentType = o.StudentTypeId.ToNameString(),
                GuardianName = o.GuardianName,
                GuardianAddress = o.GuardianAddress,
                GuardianEmail = o.GuardianEmail,
                GuardianPhone = o.GuardianPhone,
                RelationWithGuardian = o.RelationWithGuardian,
                StudentEnquiryId = o.StudentEnquiryId,

            }).OrderBy(filter.SortBy).GetPagedAsync(filter.PageNumber, filter.PageSize);

        }

        public async Task<StudentRegistrationDto> GetStudentRegistrationById(int id)
        {
            var studentRegistration = await _context.StudentRegistrations
                 .Include(n => n.Address)
                .ThenInclude(a => a.City)
                .Include(n => n.Address)
                .ThenInclude(a => a.CountryProvince)
                  .Include(n => n.Address)
                .ThenInclude(a => a.Country)
                   .Include(n => n.Address)
                .ThenInclude(a => a.Region).Include(s => s.Batch)
                  .Include(s => s.IssueCity)
                  .Include(s => s.Grade)
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (studentRegistration == null)
                throw new NotFoundException("Registered student not found");

            return new StudentRegistrationDto
            {
                Id = studentRegistration.Id,
                ImageId = studentRegistration.ImageId,
                Image = studentRegistration.ImageId > 0 ? studentRegistration.Image.Path : null,
                BatchId = studentRegistration.BatchId,
                Batch = studentRegistration.Batch.Name,
                DateOfBirthAD = studentRegistration.DateOfBirthAD,
                DateOfBirthBS = studentRegistration.DateOfBirthBS,
                FatherEmail = studentRegistration.FatherEmail,
                FatherName = studentRegistration.FatherName,
                FatherOccupation = studentRegistration.FatherOccupation,
                FatherOrganization = studentRegistration.FatherOrganization,
                FatherPhone = studentRegistration.FatherPhone,
                MotherEmail = studentRegistration.MotherEmail,
                MotherName = studentRegistration.MotherName,
                MotherOccupation = studentRegistration.MotherOccupation,
                MotherOrganization = studentRegistration.MotherOrganization,
                MotherPhone = studentRegistration.MotherPhone,
                FirstName = studentRegistration.FirstName,
                MiddleName = studentRegistration.MiddleName,
                LastName = studentRegistration.LastName,
                GenderId = studentRegistration.GenderId,
                Gender = studentRegistration.GenderId.ToNameString(),
                BloodGroup = studentRegistration.BloodGroup,
                BirthCertificateNo = studentRegistration.BirthCertificateNo,
                DateOfIssueAD = studentRegistration.DateOfIssueAD,
                DateOfIssueBS = studentRegistration.DateOfIssueBS,
                PlaceOfIssue = studentRegistration.PlaceOfIssue,
                IssueCityId = studentRegistration.IssueCityId,
                CityName = studentRegistration.IssueCity.Name,
                PassportNo = studentRegistration.PassportNo,
                ValidUpToAD = studentRegistration.ValidUpToAD,
                VisaCategory = studentRegistration.VisaCategory,
                DateOfEnrollmentAD = studentRegistration.DateOfEnrollmentAD,
                GradeId = studentRegistration.GradeId,
                Grade = studentRegistration.Grade.Name,
                Section = studentRegistration.Section,
                RegistrationNo = studentRegistration.RegistrationNo,
                RollNo = studentRegistration.RollNo,
                StudentTypeId = studentRegistration.StudentTypeId,
                StudentType = studentRegistration.StudentTypeId.ToNameString(),
                GuardianName = studentRegistration.GuardianName,
                GuardianAddress = studentRegistration.GuardianAddress,
                GuardianEmail = studentRegistration.GuardianEmail,
                GuardianPhone = studentRegistration.GuardianPhone,
                RelationWithGuardian = studentRegistration.RelationWithGuardian,
                StudentEnquiryId = studentRegistration.StudentEnquiryId,

                Address = studentRegistration.Address == null ? null : new Addresses.Models.Dtos.AddressDto
                {
                    Id = studentRegistration.Address.Id,
                    Email = studentRegistration.Address.Email,
                    CountryId = studentRegistration.Address.CountryId,
                    CountryProvinceId = studentRegistration.Address.CountryProvinceId,
                    City = studentRegistration.Address.City?.Name,
                    CityId = studentRegistration.Address.CityId,
                    RegionId = studentRegistration.Address.RegionId,
                    Country = studentRegistration.Address.Country?.Name,
                    Region = studentRegistration.Address.Region?.Name,
                    CountryProvince = studentRegistration.Address.CountryProvince?.Name,
                    Address1 = studentRegistration.Address.Address1,
                    Address2 = studentRegistration.Address.Address2,
                    ZipPostalCode = studentRegistration.Address.ZipPostalCode,
                    PhoneNumber = studentRegistration.Address.PhoneNumber,
                    SecondaryPhoneNumber = studentRegistration.Address.SecondaryPhoneNumber,
                    FaxNumber = studentRegistration.Address.FaxNumber,
                    MapLink = studentRegistration.Address.MapLink
                },

            };
        }

        public async Task<bool> DeleteStudentRegistration(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var studentRegistration = await _context.StudentRegistrations.FindAsync(id);
            if (studentRegistration == null)
                throw new BadRequestException("Invalid Registered student.");

            studentRegistration.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> InsertStudentRegistrationImage(InsertStudentRegistrationImage image)
        {
            var studentRegistration = await _context.StudentRegistrations.FirstOrDefaultAsync(v => v.Id == image.studentRegistrationId);
            if (studentRegistration == null)
                throw new BadRequestException("student Registration not found");


            // Logo
            if (image.image != null)
            {

                studentRegistration.ImageId = await _fileService.InsertFile(new Files.Models.InsertFileCommand
                {
                    EntityName = nameof(StudentRegistration),
                    File = image.image
                });

            }

            return (await _context.SaveChangesAsync()) > 0;

        }
        #endregion

        #region student fees.

        public async Task<int> InsertUpdateFee(InsertUpdateFee studentFeeCmd)
        {

            if (studentFeeCmd.Id == 0)
            {
                var newFee = new Fee
                {
                    Section = studentFeeCmd.Section,
                    RollNo = studentFeeCmd.RollNo,
                    BillingDate = studentFeeCmd.BillingDate,
                    DueDate = studentFeeCmd.DueDate,
                    Remark = studentFeeCmd.Remark,
                    BatchId = studentFeeCmd.BatchId,
                    StudentId = studentFeeCmd.StudentId,
                    GradeId = studentFeeCmd.GradeId,
                    BillingTypeId = studentFeeCmd.BillingTypeId,
                };
                _context.Fees.Add(newFee);
                await _context.SaveChangesAsync();
                return newFee.Id;
            }
            else
            {
                var existing = await _context.Fees
                                      .FirstOrDefaultAsync(i => i.Id == studentFeeCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student fee not found");

                existing.Section = studentFeeCmd.Section;
                existing.RollNo = studentFeeCmd.RollNo;
                existing.BillingDate = studentFeeCmd.BillingDate;
                existing.DueDate = studentFeeCmd.DueDate;
                existing.Remark = studentFeeCmd.Remark;
                existing.BatchId = studentFeeCmd.BatchId;
                existing.StudentId = studentFeeCmd.StudentId;
                existing.GradeId = studentFeeCmd.GradeId;
                existing.BillingTypeId = studentFeeCmd.BillingTypeId;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<FeeDto>> FeeList()
        {
            return await _context.Fees
                .AsNoTracking()
                .Select(s => new FeeDto
                {
                    Id = s.Id,
                    BillingDate = s.BillingDate,
                    DueDate = s.DueDate,
                    BillingTypeId = s.BillingTypeId,
                    RollNo = s.RollNo,
                    GradeId = s.GradeId,
                    BatchId = s.BatchId,
                    StudentId = s.StudentId,
                    Remark = s.Remark,
                    Section = s.Section,
                }).ToListAsync();
        }

        public async Task<FeeDto> FeeById(int id)
        {
            var fee = await _context.Fees
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (fee == null)
                throw new NotFoundException("fee not found");

            return new FeeDto
            {
                Id = fee.Id,
                BillingDate = fee.BillingDate,
                DueDate = fee.DueDate,
                BillingTypeId = fee.BillingTypeId,
                RollNo = fee.RollNo,
                GradeId = fee.GradeId,
                BatchId = fee.BatchId,
                StudentId = fee.StudentId,
                Remark = fee.Remark,
                Section = fee.Section,
            };
        }

        public async Task<bool> DeleteFee(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var fee = await _context.Fees.FindAsync(id);
            if (fee == null)
                throw new BadRequestException("Invalid fee.");

            fee.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region Additional student fees.

        public async Task<int> InsertUpdateAdditionalFee(InsertUpdateAdditionalFee studentFeeCmd)
        {

            if (studentFeeCmd.Id == 0)
            {
                var newAdditionalFee = new AdditionalFee
                {
                    Section = studentFeeCmd.Section,
                    RollNo = studentFeeCmd.RollNo,
                    BatchId = studentFeeCmd.BatchId,
                    StudentId = studentFeeCmd.StudentId,
                    GradeId = studentFeeCmd.GradeId,
                    BillingPeriod = studentFeeCmd.BillingPeriod,
                };
                _context.AdditionalFees.Add(newAdditionalFee);
                await _context.SaveChangesAsync();
                return newAdditionalFee.Id;
            }
            else
            {
                var existing = await _context.AdditionalFees
                                      .FirstOrDefaultAsync(i => i.Id == studentFeeCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student additional fee not found");

                existing.Section = studentFeeCmd.Section;
                existing.RollNo = studentFeeCmd.RollNo;
                existing.BillingPeriod = studentFeeCmd.BillingPeriod;
                existing.BatchId = studentFeeCmd.BatchId;
                existing.StudentId = studentFeeCmd.StudentId;
                existing.GradeId = studentFeeCmd.GradeId;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<AdditionalFeeDto>> AdditionalFeeList()
        {
            return await _context.AdditionalFees
                .AsNoTracking()
                .Select(s => new AdditionalFeeDto
                {
                    Id = s.Id,
                    BillingPeriod = s.BillingPeriod,
                    RollNo = s.RollNo,
                    GradeId = s.GradeId,
                    BatchId = s.BatchId,
                    StudentId = s.StudentId,
                    Section = s.Section,
                }).ToListAsync();
        }

        public async Task<AdditionalFeeDto> AdditionalFeeById(int id)
        {
            var additionalFee = await _context.AdditionalFees
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (additionalFee == null)
                throw new NotFoundException("additional Fee not found");

            return new AdditionalFeeDto
            {
                Id = additionalFee.Id,
                BillingPeriod = additionalFee.BillingPeriod,
                RollNo = additionalFee.RollNo,
                GradeId = additionalFee.GradeId,
                BatchId = additionalFee.BatchId,
                StudentId = additionalFee.StudentId,
                Section = additionalFee.Section,
            };
        }

        public async Task<bool> DeleteAdditionalFee(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var additionalFee = await _context.AdditionalFees.FindAsync(id);
            if (additionalFee == null)
                throw new BadRequestException("Invalid additional fee.");

            additionalFee.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region student fees payment.

        public async Task<int> InsertUpdateFeePayment(InsertUpdateFeePayment studentFeeCmd)
        {

            if (studentFeeCmd.Id == 0)
            {
                var newFeePayment = new FeePayment
                {
                    AccountName = studentFeeCmd.AccountName,
                    AccountNumber = studentFeeCmd.AccountNumber,
                    AdditionalCharge = studentFeeCmd.AdditionalCharge,
                    Branch = studentFeeCmd.Branch,
                    Amount = studentFeeCmd.Amount,
                    Discount = studentFeeCmd.Discount,
                    DiscountAmount = studentFeeCmd.DiscountAmount,
                    PaymentTypeId = studentFeeCmd.PaymentTypeId,
                    Notes = studentFeeCmd.Notes,
                    ReferenceID = studentFeeCmd.ReferenceID,
                    ServiceProviderName = studentFeeCmd.ServiceProviderName,
                    TotalAmount = studentFeeCmd.TotalAmount,
                    TransactionDate = studentFeeCmd.TransactionDate,
                    TransactionID = studentFeeCmd.TransactionID
                };
                _context.FeePayments.Add(newFeePayment);
                await _context.SaveChangesAsync();
                return newFeePayment.Id;
            }
            else
            {
                var existing = await _context.FeePayments
                                      .FirstOrDefaultAsync(i => i.Id == studentFeeCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student fee payment not found");

                existing.AccountName = studentFeeCmd.AccountName;
                existing.AccountNumber = studentFeeCmd.AccountNumber;
                existing.AdditionalCharge = studentFeeCmd.AdditionalCharge;
                existing.Branch = studentFeeCmd.Branch;
                existing.Amount = studentFeeCmd.Amount;
                existing.Discount = studentFeeCmd.Discount;
                existing.DiscountAmount = studentFeeCmd.DiscountAmount;
                existing.PaymentTypeId = studentFeeCmd.PaymentTypeId;
                existing.Notes = studentFeeCmd.Notes;
                existing.ReferenceID = studentFeeCmd.ReferenceID;
                existing.ServiceProviderName = studentFeeCmd.ServiceProviderName;
                existing.TotalAmount = studentFeeCmd.TotalAmount;
                existing.TransactionDate = studentFeeCmd.TransactionDate;
                existing.TransactionID = studentFeeCmd.TransactionID;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<FeePaymentDto>> StudentFeePaymentList()
        {
            return await _context.FeePayments
                .AsNoTracking()
                .Select(s => new FeePaymentDto
                {
                    Id = s.Id,
                    AccountName = s.AccountName,
                    AccountNumber = s.AccountNumber,
                    AdditionalCharge = s.AdditionalCharge,
                    Branch = s.Branch,
                    Amount = s.Amount,
                    Discount = s.Discount,
                    DiscountAmount = s.DiscountAmount,
                    PaymentTypeId = s.PaymentTypeId,
                    Notes = s.Notes,
                    ReferenceID = s.ReferenceID,
                    ServiceProviderName = s.ServiceProviderName,
                    TotalAmount = s.TotalAmount,
                    TransactionDate = s.TransactionDate,
                    TransactionID = s.TransactionID
                }).ToListAsync();
        }

        public async Task<FeePaymentDto> StudentFeePaymentById(int id)
        {
            var feePayment = await _context.FeePayments
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (feePayment == null)
                throw new NotFoundException("Fee payment not found");

            return new FeePaymentDto
            {
                Id = feePayment.Id,
                AccountName = feePayment.AccountName,
                AccountNumber = feePayment.AccountNumber,
                AdditionalCharge = feePayment.AdditionalCharge,
                Branch = feePayment.Branch,
                Amount = feePayment.Amount,
                Discount = feePayment.Discount,
                DiscountAmount = feePayment.DiscountAmount,
                PaymentTypeId = feePayment.PaymentTypeId,
                Notes = feePayment.Notes,
                ReferenceID = feePayment.ReferenceID,
                ServiceProviderName = feePayment.ServiceProviderName,
                TotalAmount = feePayment.TotalAmount,
                TransactionDate = feePayment.TransactionDate,
                TransactionID = feePayment.TransactionID
            };
        }

        public async Task<bool> DeleteFeePayment(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var feePayment = await _context.FeePayments.FindAsync(id);
            if (feePayment == null)
                throw new BadRequestException("Invalid fee payment.");

            feePayment.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion

        #region bad dept.

        public async Task<int> InsertUpdateBadDept(InsertUpdateBadDept studentFeeCmd)
        {

            if (studentFeeCmd.Id == 0)
            {
                var newBadDept = new BadDept
                {
                    ApprovedBy = studentFeeCmd.ApprovedBy,
                    BadDeptNo = studentFeeCmd.BadDeptNo,
                    BadDeptTypeId = studentFeeCmd.BadDeptTypeId,
                    BatchId = studentFeeCmd.BatchId,
                    Amount = studentFeeCmd.Amount,
                    Date = studentFeeCmd.Date,
                    Due = studentFeeCmd.Due,
                    EnteredBy = studentFeeCmd.EnteredBy,
                    GradeId = studentFeeCmd.GradeId,
                    Remark = studentFeeCmd.Remark,
                    RollNo = studentFeeCmd.RollNo,
                    Section = studentFeeCmd.Section,
                    StudentId = studentFeeCmd.StudentId,
                };
                _context.BadDepts.Add(newBadDept);
                await _context.SaveChangesAsync();
                return newBadDept.Id;
            }
            else
            {
                var existing = await _context.BadDepts
                                      .FirstOrDefaultAsync(i => i.Id == studentFeeCmd.Id);
                if (existing == null)
                    throw new NotFoundException("student bad dept not found");

                existing.ApprovedBy = studentFeeCmd.ApprovedBy;
                existing.BadDeptNo = studentFeeCmd.BadDeptNo;
                existing.BadDeptTypeId = studentFeeCmd.BadDeptTypeId;
                existing.BatchId = studentFeeCmd.BatchId;
                existing.Amount = studentFeeCmd.Amount;
                existing.Date = studentFeeCmd.Date;
                existing.Due = studentFeeCmd.Due;
                existing.EnteredBy = studentFeeCmd.EnteredBy;
                existing.GradeId = studentFeeCmd.GradeId;
                existing.Remark = studentFeeCmd.Remark;
                existing.RollNo = studentFeeCmd.RollNo;
                existing.Section = studentFeeCmd.Section;
                existing.StudentId = studentFeeCmd.StudentId;

                if (await _context.SaveChangesAsync() > 0)
                    return existing.Id;
            }
            return 0;
        }

        public async Task<List<BadDeptDto>> BadDeptList()
        {
            return await _context.BadDepts
                .AsNoTracking()
                .Select(s => new BadDeptDto
                {
                    Id = s.Id,
                    ApprovedBy = s.ApprovedBy,
                    BadDeptNo = s.BadDeptNo,
                    BadDeptTypeId = s.BadDeptTypeId,
                    BatchId = s.BatchId,
                    Amount = s.Amount,
                    Date = s.Date,
                    Due = s.Due,
                    EnteredBy = s.EnteredBy,
                    GradeId = s.GradeId,
                    Remark = s.Remark,
                    RollNo = s.RollNo,
                    Section = s.Section,
                    StudentId = s.StudentId,
                }).ToListAsync();
        }

        public async Task<BadDeptDto> BadDeptById(int id)
        {
            var badDept = await _context.BadDepts
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == id);

            if (badDept == null)
                throw new NotFoundException("Bad Dept not found");

            return new BadDeptDto
            {
                Id = badDept.Id,
                ApprovedBy = badDept.ApprovedBy,
                BadDeptNo = badDept.BadDeptNo,
                BadDeptTypeId = badDept.BadDeptTypeId,
                BatchId = badDept.BatchId,
                Amount = badDept.Amount,
                Date = badDept.Date,
                Due = badDept.Due,
                EnteredBy = badDept.EnteredBy,
                GradeId = badDept.GradeId,
                Remark = badDept.Remark,
                RollNo = badDept.RollNo,
                Section = badDept.Section,
                StudentId = badDept.StudentId,
            };
        }

        public async Task<bool> DeleteBadDept(int id)
        {
            //if (!_currentUserService.HavePermission(Permissions.DeleteStores))
            //    throw new ForbiddenException();

            var badDept = await _context.BadDepts.FindAsync(id);
            if (badDept == null)
                throw new BadRequestException("Invalid Bad Dept.");

            badDept.IsDeleted = true;
            return (await _context.SaveChangesAsync() > 0);
        }

        #endregion
    }
}
