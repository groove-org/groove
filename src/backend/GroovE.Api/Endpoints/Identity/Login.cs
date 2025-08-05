using FluentValidation;
using GroovE.Api.Common;
using GroovE.Infrastructure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroovE.Api.Endpoints.Identity;

public class Login(
    UserManager<User> userManager,
    IOptions<JwtSettings> jwtSettings) : IEndpoint
{
    public record CustomLoginRequest(string Email, string Password, bool RememberMe);
    public record CustomLoginResponse(string Token);
    public abstract class RequestValidator : AbstractValidator<CustomLoginRequest>
    {
        public RequestValidator()
        {
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.Password).NotEmpty();
        }
    }

    public void Map(IEndpointRouteBuilder app) => app
        .MapPost("/login", async (CustomLoginRequest request) => await Handle(request))
        .WithRequestValidation<CustomLoginRequest>()
        .WithSummary("Logs in a user and returns a JWT token")
        .WithTags("Identity");

    public async Task<Results<Ok<CustomLoginResponse>, UnauthorizedHttpResult>> Handle(CustomLoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            return TypedResults.Unauthorized();

        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Value.Secret);

        var expiryDate = request.RememberMe
            ? jwtSettings.Value.ExpirationInHoursRememberMe
            : jwtSettings.Value.ExpirationInHoursDefault;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                .. roles.Select(role => new Claim(ClaimTypes.Role, role))
            ]),
            Expires = DateTime.UtcNow.AddHours(expiryDate),
            Issuer = jwtSettings.Value.Issuer,
            Audience = jwtSettings.Value.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return TypedResults.Ok(new CustomLoginResponse(jwt));
    }
}
