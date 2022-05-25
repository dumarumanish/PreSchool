using PreSchool.Application.Models.AppUsers.Queries;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;
using PreSchool.Application.Services.AppUsers.Models.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.AppUsers
{
    public interface IAppUserService
    {
        Task<bool> AddRemoveAppUserRole(AppUserRoleCommand changeUserRole, bool isRoleAdd = true);
        Task<bool> ChangeAppUserPassword(ChangePasswordCommand changePassword);
        Task<bool> DeleteAcadamic(int appUserId, int id);
        Task<bool> DeleteEmergencyContacts(int appUserId, int id);
        Task<bool> DeleteExperience(int appUserId, int id);
        Task<bool> DeleteInternalUserDocuments(int appUserId, int id);
        Task<bool> ForgotPassword(ForgotPasswordCommand forgotPassword);
        Task<AppUserDto> GetAppUserById(int appUserId);
        Task<List<AppUserListDto>> GetAppUsers(AppUserFilter filter);
        List<EnumValue> GetAppUserType();
        Task<int> InsertInternalUser(InsertAppUser appUserCommand);
        Task<int> InsertUpdateInternalUserAcademic(InsertUpdateInternalUserAcademic acadamicCmd);
        Task<bool> InsertUpdateInternalUserDocuments(InsertUpdateInternalUserDocuments image);
        Task<int> InsertUpdateInternalUserEmergencyContact(InsertUpdateInternalUserEmergencyContact appUserCommand);
        Task<int> InsertUpdateInternalUserExperience(InsertUpdateInternalUserExperience appUserCommand);
        Task<bool> ResendVerificationCode(ResendVerificationCode username);
        Task<bool> ResetPassword(ResetPasswordCommand resetPassword);
        Task<bool> UpdateAppUser(UpdateAppUser appUserCommand);
        Task<ValidUserDto> ValidateLogin(Login loginCommand);
    }
}