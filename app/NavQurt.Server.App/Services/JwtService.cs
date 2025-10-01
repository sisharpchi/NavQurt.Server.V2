using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace NavQurt.Server.App.Services;

public class JwtService
{
    public string? GetRoleFromToken(string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role ||
                                                                c.Type.Equals("role", StringComparison.OrdinalIgnoreCase) ||
                                                                c.Type.Equals("roles", StringComparison.OrdinalIgnoreCase));
            if (roleClaim is not null)
            {
                return roleClaim.Value;
            }

            var multipleRoles = jwtToken.Claims.Where(c => c.Type == "roles").Select(c => c.Value).ToList();
            return multipleRoles.FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
}
