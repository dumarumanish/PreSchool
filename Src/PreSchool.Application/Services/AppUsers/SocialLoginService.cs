using PreSchool.Application.Exceptions;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Application.Services.Emails;
using PreSchool.Data.Entities.AppUsers;
using PreSchool.Data.Enumerations;
using PreSchool.EmailTemplates.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.AppUsers
{
    public class SocialLoginService : ISocialLoginService
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        private readonly IJwtService _jwtService;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public SocialLoginService(
            IOptions<FacebookAuthSettings> facebookAuthSettings,
            IApplicationDbContext context,
             IOptions<AppSettings> appSettings,
            IJwtService jwtService,
            IDateTime dateTime,
            ICurrentUserService currentUserService,
            IEmailService emailService

            )
        {
            _facebookAuthSettings = facebookAuthSettings.Value;
            _context = context;
            _dateTime = dateTime;
            _currentUserService = currentUserService;
            _jwtService = jwtService;
            _appSettings = appSettings.Value;
            _emailService = emailService;

        }
        public async Task<ValidUserDto> Facebook(SocialLoginCommand model)
        {

            var userInfo = await ValidateFacebookToken(model.AccessToken);

            if (userInfo == null)
                return new ValidUserDto
                {
                    IsLoginValid = false,
                    Remarks = "Invalid facebook token"
                };

            if (string.IsNullOrWhiteSpace(userInfo.Email))
                throw new BadRequestException("Cannot fetch email from your account, Email permission required.");

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _context.AppUsers
                 .Include(a => a.AppUserRoles)
                   .Include(a => a.AppUserLoginHistories)
                .FirstOrDefaultAsync(a => a.Username == userInfo.Email);

            if (user == null)
            {
                user = new AppUser
                {
                    Active = true,
                    AdminComment = "",
                    CreatedBy = 1,
                    CreatedOn = _dateTime.Now,
                    CannotLoginUntilDate = null,
                    FacebookUserId = userInfo.Id,
                    IsDeleted = false,
                    IsSystemAccount = false,
                    RequireReLogin = false,
                    Username = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    IsEmailVerified = true,
                    Email = userInfo.Email,
                    AppUserTypeId = (int)AppUserTypeEnum.Customer,

                    AppUserRoles = new List<AppUserRole>
                    {
                        new AppUserRole
                        {
                            CreatedBy = 1,
                            CreatedOn = _dateTime.Now,
                            IsDeleted = false,
                            RoleId = (int)ReservedRoles.Customer,
                        }
                    },
                    //CustomerAppUserMapping = new Data.Entities.Customers.CustomerAppUserMapping
                    //{
                    //    Customer = new Data.Entities.Customers.Customer
                    //    {
                    //        CompanyName = null,
                    //        DateOfBirth = null,
                    //        Email = userInfo.Email,
                    //        FirstName = userInfo.FirstName,
                    //        MiddleName = null,
                    //        LastName = userInfo.LastName,
                    //        GenderId = null,
                    //        IsIndividual = true,
                    //        PhoneNumber = null,
                    //        PanNumber = null,

                    //    }
                    //}

                };

                _context.AppUsers.Add(user);
                await _context.SaveChangesAsync();
                //email.
                var welcomeCustomerOfSocialMediaEmailViewModel = new WelcomeCustomerOfSocialMediaEmailViewModel(
                                   user.FirstName,
                                    user.Email
                                     );

                _emailService.WelcomeCustomerOfSocialMedia(welcomeCustomerOfSocialMediaEmailViewModel);

            }
            else
            {
                if (!user.IsEmailVerified)
                {
                    user.IsEmailVerified = true;
                    await _context.SaveChangesAsync();

                }
            }
            // Check if the existing user has facebook id
            if (user.FacebookUserId == null)
            {
                user.FacebookUserId = userInfo.Id;
                await _context.SaveChangesAsync();

            }


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
         
            var userClaims = new UsersClaims(user.Id, user.Username, user.AppUserTypeId, permissions); ;


            return new ValidUserDto
            {
                IsLoginValid = true,
                Remarks = null,
                RequiredPasswordChange = false,
                Token = _jwtService.GenerateToken(userClaims),
                 AppUserTypeId= user.AppUserTypeId,
                EmailAddress = user.Email,
                FullName = user.FirstName,
                IsEmailVerified = user.IsEmailVerified,
                IsPhoneVerified = user.IsPhoneVerified,
                PhoneNumber = user.PhoneNumber,

            };
        }

        public async Task<ValidUserDto> Google(SocialLoginCommand model)
        {
            var userInfo = await ValidateGoogleToken(model.AccessToken);
            if (userInfo == null)
                return new ValidUserDto
                {
                    IsLoginValid = false,
                    Remarks = "Invalid google token"
                };

            if (string.IsNullOrWhiteSpace(userInfo.Email))
                throw new BadRequestException("Cannot fetch email from your account, Email permission required.");

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await _context.AppUsers
                       .Include(a => a.AppUserRoles)
                                .Include(a => a.AppUserLoginHistories)
                .FirstOrDefaultAsync(a => a.Username == userInfo.Email);

            if (user == null)
            {
                user = new AppUser
                {
                    Active = true,
                    AdminComment = "",
                    CreatedBy = 1,
                    CreatedOn = _dateTime.Now,
                    CannotLoginUntilDate = null,
                    GoogleUserId = userInfo.Id,
                    IsDeleted = false,
                    IsSystemAccount = false,
                    RequireReLogin = false,
                    Username = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    IsEmailVerified = true,
                    Email = userInfo.Email,
                    AppUserTypeId = (int)AppUserTypeEnum.Customer,

                    AppUserRoles = new List<AppUserRole>
                    {
                        new AppUserRole
                        {
                            CreatedBy = 1,
                            CreatedOn = _dateTime.Now,
                            IsDeleted = false,
                            RoleId = (int)ReservedRoles.Customer,
                        }
                    },
                      //CustomerAppUserMapping = new Data.Entities.Customers.CustomerAppUserMapping
                      //{
                      //    Customer = new Data.Entities.Customers.Customer
                      //    {
                      //        CompanyName = null,
                      //        DateOfBirth = null,
                      //        Email = userInfo.Email,
                      //        FirstName = userInfo.FirstName,
                      //        MiddleName = null,
                      //        LastName = userInfo.LastName,
                      //        GenderId = null,
                      //        IsIndividual = true,
                      //        PhoneNumber = null,
                      //        PanNumber = null,

                      //    }
                      //}

                };

                _context.AppUsers.Add(user);
                await _context.SaveChangesAsync();
                //email.
                var welcomeCustomerOfSocialMediaEmailViewModel = new WelcomeCustomerOfSocialMediaEmailViewModel(
                        user.FirstName,
                         user.Email
                          );

                _emailService.WelcomeCustomerOfSocialMedia(welcomeCustomerOfSocialMediaEmailViewModel);
            }
            else
            {
                if (!user.IsEmailVerified)
                {
                    user.IsEmailVerified = true;
                    await _context.SaveChangesAsync();
                }

            }

            // Check if the existing user has facebook id
            if (user.GoogleUserId == null)
            {
                user.GoogleUserId = userInfo.Id;
                await _context.SaveChangesAsync();

            }


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
            var userClaims = new UsersClaims(user.Id, user.Username, user.AppUserTypeId, permissions); ;


            return new ValidUserDto
            {
                IsLoginValid = true,
                Remarks = null,
                RequiredPasswordChange = false,
                Token = _jwtService.GenerateToken(userClaims),
                AppUserTypeId = user.AppUserTypeId,
                FullName = user.FirstName,
                IsEmailVerified = user.IsEmailVerified,
                EmailAddress = user.Email,
                IsPhoneVerified = user.IsPhoneVerified,
                PhoneNumber = user.PhoneNumber,
            };
        }

        private async Task<FacebookUserData> ValidateFacebookToken(string token)
        {
            try
            {
                // 1.generate an app access token
                var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_facebookAuthSettings.AppId}&client_secret={_facebookAuthSettings.AppSecret}&grant_type=client_credentials");
                var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
                // 2. validate the user access token
                var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={token}&access_token={appAccessToken.AccessToken}");
                var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

                if (!userAccessTokenValidation.Data.IsValid)
                {
                    return null;
                }

                // 3. we've got a valid token so we can request user data from fb
                var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={token}");
                return JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);
            }
            catch (Exception)
            {

                return null;
            }
        }

        private async Task<GoogleUserData> ValidateGoogleToken(string providerToken)
        {

            var GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";

            var requestUri = new Uri(string.Format(GoogleApiTokenInfoUrl, providerToken));

            HttpResponseMessage httpResponseMessage;
            try
            {
                httpResponseMessage = await Client.GetAsync(requestUri);
            }
            catch (Exception)
            {
                return null;
            }

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleUserData>(response);


        }
    }


}
