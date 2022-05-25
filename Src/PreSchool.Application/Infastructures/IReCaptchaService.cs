using System.Threading.Tasks;

namespace PreSchool.Infrastructure.Services.ReCaptcha
{
    public interface IReCaptchaService
    {
        Task<bool> VerifyReCaptcha(string token);
    }
}