using FluentValidation;
using GroovE.Api.Endpoints.Identity;
using GroovE.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroovE.Api.Endpoints;

[Route("api/[controller]")]
[ApiController]
public class AuthController(
    UserManager<User> userManager,
    JwtSettings jwtSettings) : ControllerBase
{
    public record UserLoginRequest(string Email, string Password, bool RememberMe);
    public record UserLoginResponse(string Token);

    public class LoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(r => r.Email).NotEmpty().EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
        }
    }

    /// <summary>
    /// Logs in a user and returns a JWT token.
    /// </summary>
    /// <param name="request">The login request containing email, password, and remember me option.</param>
    /// <returns>A JWT token if login is successful, or Unauthorized if login fails.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserLoginResponse>> Login(UserLoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized();

        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        var expiryDate = request.RememberMe
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

        return Ok(new UserLoginResponse(jwt));
    }
}
