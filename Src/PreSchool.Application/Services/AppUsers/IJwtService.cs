using PreSchool.Application.Models;

namespace PreSchool.Application.Services.AppUsers
{
    public interface IJwtService
    {
        string GenerateToken(UsersClaims userClaims,int expiryDays = 7);
    }
}