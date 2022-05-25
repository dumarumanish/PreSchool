using System.Threading.Tasks;

namespace PreSchool.Application.Services.SMSSenders
{
    public interface ISMSSenderService
    {
        string SendSMS(string from, string token, string to, string text);
        Task<bool> SendSMS(string to, string text);
    }
}