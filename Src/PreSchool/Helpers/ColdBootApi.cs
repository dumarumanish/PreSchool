using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PreSchool.Application.Models;
using PreSchool.Data.Enumerations;

namespace PreSchool
{
    public class ColdBootApi : BackgroundService
    {
        private readonly AppSettings _appSettings;

        public ColdBootApi(
            IOptions<AppSettings> appSettings
            )
        {
            _appSettings = appSettings.Value;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           
            try
            {
                var baseAddress = new Uri(_appSettings.BaseUrl);
                var coldBootUrls = new List<string>
                {
                    //"api/Citizens",

                };

                using (var httpClient = new HttpClient { BaseAddress = baseAddress })
                {
                    var token = GenerateToken();
                    httpClient.DefaultRequestHeaders.Authorization
                           = new AuthenticationHeaderValue("Bearer", token);
                    foreach (var url in coldBootUrls)
                    {
                        using (var response = await httpClient.GetAsync(url))
                        {
                            //string responseData = await response.Content.ReadAsStringAsync();
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error(ex, "ColdBootError");
            }
        }


        private string GenerateToken()
        {
            var permissions = ((int)Permissions.TotalAccess).ToString();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.AuthenticationKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                       {
                          new Claim(CustomClaimType.Permissions, permissions),
                       }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
