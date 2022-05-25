using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PreSchool.Application.Models;

namespace PreSchool.Application.Services.AppUsers
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;

        public JwtService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }


        /// <summary>
        /// Generate token for the user
        /// </summary>
        /// <returns></returns>

        public string GenerateToken(UsersClaims userClaims,int expiryDays = 7)
        {
            if (userClaims == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.AuthenticationKey);
            //  var key = Encoding.ASCII.GetBytes("Get this key form appsettings");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                       {
                          new Claim(CustomClaimType.AppUserId, userClaims.UserId.ToString()),
                          new Claim(CustomClaimType.Permissions, userClaims.Permissions),
                          new Claim(ClaimTypes.Name, userClaims.Username.ToString()),
                          new Claim(CustomClaimType.AppUserType, userClaims.UserType.ToString()),

                       }),
                Expires = DateTime.UtcNow.AddDays(expiryDays),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
