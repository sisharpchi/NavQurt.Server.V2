using NavQurt.Server.Application.Dto_s;
using System.Security.Claims;

namespace NavQurt.Server.Application.Helpers;

public interface ITokenService
{
    public string GenerateToken(UserGetDto user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}






