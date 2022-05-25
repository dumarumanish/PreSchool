using System.Threading.Tasks;
using PreSchool.Application.Services.AppUsers.Models.Commands;
using PreSchool.Application.Services.AppUsers.Models.Dtos;

namespace PreSchool.Application.Services.AppUsers
{
    public interface ISocialLoginService
    {
        Task<ValidUserDto> Facebook(SocialLoginCommand model);
        Task<ValidUserDto> Google(SocialLoginCommand model);
    }
}