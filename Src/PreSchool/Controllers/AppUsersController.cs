using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PreSchool.Application.Exceptions;
using PreSchool.Application.Models.AppUsers.Queries;
using PreSchool.Application.Services.AppUsers;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Data.Enumerations;
using PreSchool.Application.Services.Addresses.Models.Filters;
using PreSchool.Application;
using PreSchool.Application.Services.AppUsers.Models.Queries;

namespace PreSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {

        private readonly IAppUserService _appUserService;
        private readonly ISocialLoginService _socialLoginService;

        public AppUsersController(IAppUserService appUserService, ISocialLoginService socialLoginService)
        {
            _appUserService = appUserService;
            _socialLoginService = socialLoginService;
        }


        /// <summary>
        /// Login to get token
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ValidUserDto> Login([FromBody] Login command)
        {
            return await _appUserService.ValidateLogin(command);
        }

        [HttpPost("Facebook")]
        [AllowAnonymous]
        public async Task<ValidUserDto> FacebookLogin([FromBody] SocialLoginCommand command)
        {
            return await _socialLoginService.Facebook(command);
        }

        [HttpPost("Google")]
        [AllowAnonymous]
        public async Task<ValidUserDto> GoogleLogin([FromBody] SocialLoginCommand command)
        {
            return await _socialLoginService.Google(command);
        }

        /// <summary>
        /// Insert appuser 
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Id of the user</returns>
        /// <remarks>
        /// AppUserTypeId = 1 (internal) => LimitedToStores not required
        /// AppUserTYpeId = 2 (Store Users) => LimitedToStores & LimitedToStores.AddStores required
        /// </remarks>
        [HttpPost]
        public async Task<int> InsertAppUser([FromBody] InsertAppUser command)
        {
            return await _appUserService.InsertInternalUser(command);
        }

        /// <summary>
        /// Update the existing appuser, can be used for any type of user
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<bool> UpdateAppUser(UpdateAppUser command)
        {
            return await _appUserService.UpdateAppUser(command);

        }

        #region Academic
        [HttpPost("{appUserId}/Academic")]
        public async Task<int> InsertInternalUserAcademic(int appUserId, [FromForm] InsertUpdateInternalUserAcademic appUserCommand)
        {

            if (appUserId != appUserCommand.AppUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            appUserCommand.Id = 0;

            return await _appUserService.InsertUpdateInternalUserAcademic(appUserCommand);
        }

        [HttpPut("{appUserId}/EmergencyContacts/{id}")]
        public async Task<int> UpdateInternalUserAcademic(int appUserId, int id, [FromForm] InsertUpdateInternalUserAcademic appUserCommand)
        {
            if (appUserId != appUserCommand.AppUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            if (id != appUserCommand.Id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _appUserService.InsertUpdateInternalUserAcademic(appUserCommand);
        }


        [HttpDelete("{appUserId}/EmergencyContacts/{id}")]
        public async Task<bool> DeleteAcadamic(int appUserId, int id)
        {
            return await _appUserService.DeleteAcadamic(appUserId, id);
        }

        #endregion


        #region EmergencyContacts
        [HttpPost("{appUserId}/EmergencyContacts")]
        public async Task<int> InsertInternalUserEmergencyContact(int appUserId, [FromForm] InsertUpdateInternalUserEmergencyContact appUserCommand)
        {

            if (appUserId != appUserCommand.AppUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            appUserCommand.Id = 0;

            return await _appUserService.InsertUpdateInternalUserEmergencyContact(appUserCommand);
        }

        [HttpPut("{appUserId}/EmergencyContacts/{id}")]
        public async Task<int> UpdateInternalUserEmergencyContact(int appUserId, int id, [FromForm] InsertUpdateInternalUserEmergencyContact appUserCommand)
        {
            if (appUserId != appUserCommand.AppUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            if (id != appUserCommand.Id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _appUserService.InsertUpdateInternalUserEmergencyContact(appUserCommand);
        }


        [HttpDelete("{appUserId}/EmergencyContacts/{id}")]
        public async Task<bool> DeleteEmergencyContacts(int appUserId, int id)
        {
            return await _appUserService.DeleteEmergencyContacts(appUserId, id);
        }

        #endregion

        #region Experience
        [HttpPost("{appUserId}/Experience")]
        public async Task<int> InsertInternalUserExperience(int appUserId, [FromForm] InsertUpdateInternalUserExperience appUserCommand)
        {

            if (appUserId != appUserCommand.AppUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            appUserCommand.Id = 0;

            return await _appUserService.InsertUpdateInternalUserExperience(appUserCommand);
        }

        [HttpPut("{appUserId}/Experience/{id}")]
        public async Task<int> UpdateInternalUserExperience(int appUserId, int id, [FromForm] InsertUpdateInternalUserExperience appUserCommand)
        {
            if (appUserId != appUserCommand.AppUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            if (id != appUserCommand.Id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _appUserService.InsertUpdateInternalUserExperience(appUserCommand);
        }


        [HttpDelete("{appUserId}/Experience/{id}")]
        public async Task<bool> DeleteExperience(int appUserId, int id)
        {
            return await _appUserService.DeleteExperience(appUserId, id);
        }

        #endregion

        #region Documents
        [HttpPost("{appUserId}/Documents/Images")]
        public async Task<bool> InsertInternalUserDocuments(int appUserId, [FromForm] InsertUpdateInternalUserDocuments insertImage)
        {
            if (insertImage == null)
                throw new BadRequestException("Image is required");

            if (appUserId != insertImage.appUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            insertImage.id = 0;

            return await _appUserService.InsertUpdateInternalUserDocuments(insertImage);
        }

        [HttpPut("{appUserId}/Documents/Images/{id}")]
        public async Task<bool> UpdateInternalUserDocuments(int appUserId, int id, [FromForm] InsertUpdateInternalUserDocuments insertImage)
        {
            if (insertImage == null)
                throw new BadRequestException("Image is required");

            if (appUserId != insertImage.appUserId)
                throw new BadRequestException("Invalid appuser id, appuser id doesnot match");

            if (id != insertImage.id)
                throw new BadRequestException("Invalid  id,  id doesnot match");

            if (id == 0)
                throw new BadRequestException("Invalid  id,  id cannot be 0");

            return await _appUserService.InsertUpdateInternalUserDocuments(insertImage);
        }

     
        [HttpDelete("{appUserId}/Documents/Images/{id}")]
        public async Task<bool> DeleteInternalUserDocuments(int appUserId, int id)
        {
            return await _appUserService.DeleteInternalUserDocuments(appUserId, id);
        }

        #endregion

        [HttpGet]
        public async Task<List<AppUserListDto>> GetAppUsers([FromQuery] AppUserFilter filter)
        {
            return await _appUserService.GetAppUsers(filter);
        }
        [HttpGet("{id}")]
        public async Task<AppUserDto> GetAppUserById(int id)
        {
            return await _appUserService.GetAppUserById(id);
        }

        [HttpGet("Type")]
        public List<EnumValue> AppUserType()
        {
            return _appUserService.GetAppUserType();
        }

        [HttpPost("ChangePassword")]
        public async Task<bool> ChangePassword(ChangePasswordCommand changePassword)
        {
            return await _appUserService.ChangeAppUserPassword(changePassword);
        }

        [HttpPost("ForgotPassword")]
        public async Task<bool> ForgotPassword(ForgotPasswordCommand forgotPassword)
        {
            return await _appUserService.ForgotPassword(forgotPassword);
        }

        [HttpPost("ResendVerificationCode")]
        public async Task<bool> ResendVerificationCode(ResendVerificationCode resend)
        {
            return await _appUserService.ResendVerificationCode(resend);
        }

        [HttpPost("ResetPassword")]
        public async Task<bool> ResetPassword(ResetPasswordCommand resetPassword)
        {
            return await _appUserService.ResetPassword(resetPassword);
        }

        [HttpPost("{id}/Roles")]
        public async Task<bool> AddUserRole(int id, AppUserRoleCommand changeUserRole)
        {
            if (id != changeUserRole?.AppUserId)
                throw new BadRequestException("UserId doesnot matched with url");


            return await _appUserService.AddRemoveAppUserRole(changeUserRole, true);
        }

        [HttpDelete("{id}/Roles/{roleId}")]
        public async Task<bool> RemoveUserRole(int id, int roleId)
        {
            var roleCommand = new AppUserRoleCommand
            {
                AppUserId = id,
                RoleId = roleId,

            };
            return await _appUserService.AddRemoveAppUserRole(roleCommand, false);
        }

    }
}