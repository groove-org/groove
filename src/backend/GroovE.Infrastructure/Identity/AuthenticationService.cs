using GroovE.Application.UseCases.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroovE.Infrastructure.Identity;

internal class AuthenticationService(UserManager<User> userManager, IOptions<JwtConfiguration> jwtSettings) : IAuthenticationService
{
    private readonly JwtConfiguration jwtSettings = jwtSettings.Value;

    public async Task<string> LoginUser(string email, string password, bool rememberMe)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null || !await userManager.CheckPasswordAsync(user, password))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        var expiryDate = rememberMe
            ? jwtSettings.ExpirationInHoursRememberMe
            : jwtSettings.ExpirationInHoursDefault;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                .. roles.Select(role => new Claim(ClaimTypes.Role, role))
            ]),
            Expires = DateTime.UtcNow.AddHours(expiryDate),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }
}
