using Services.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services.Interfaces.AuthServices
{
    public interface IJwtService
    {
        string genrateJwtToken(UserDataModel user);

        string genrateJwtTokenForSendMail(List<Claim> claims, DateTime expries);

        bool validateToken(String token, out JwtSecurityToken jwtToken);
    }
}
