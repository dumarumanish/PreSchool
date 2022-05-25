using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using PreSchool.Application.Infastructures;
using PreSchool.Application.Models;
using PreSchool.Application.Services.ReCaptcha.Models;

namespace PreSchool.Infrastructure.Services.ReCaptcha
{
    public class ReCaptchaService : IReCaptchaService
    {
        private readonly ReCaptchaSettings _reCaptchaSetting;

        public ReCaptchaService(IOptions<ReCaptchaSettings> reCaptchaSetting)
        {
            _reCaptchaSetting = reCaptchaSetting.Value;

        }

        public async Task<bool> VerifyReCaptcha(string token)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify?secret={ _reCaptchaSetting.SecretKey}&response={token}";
                //var paramenters = new
                //{
                //    secret = _reCaptchaSetting.SecretKey,
                //    response = token,
                //};

                //var content = new StringContent(JsonConvert.SerializeObject(paramenters), Encoding.UTF8,
                //      "application/json");
                HttpResponseMessage response = await client.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var recaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(responseString);
                    return recaptchaResponse.Success;
                }

                return false;
            }
        }
    }
}
