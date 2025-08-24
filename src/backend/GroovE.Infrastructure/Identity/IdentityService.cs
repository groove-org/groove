using GroovE.Application.Identity;
using GroovE.Application.UseCases.Identity;
using GroovE.Application.UseCases.Identity.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GroovE.Infrastructure.Identity;

internal class IdentityService(UserManager<User> userManager, IOptions<JwtConfiguration> jwtSettings, SignInManager<User> signInManager) : IIdentityService
{
    private readonly JwtConfiguration jwtSettings = jwtSettings.Value;

    public async Task<LoginResponseDto> LoginUserAsync(string email, string password, bool rememberMe)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null || !await userManager.CheckPasswordAsync(user, password))
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (await userManager.GetTwoFactorEnabledAsync(user))
        {
            return new LoginResponseDto(null, true);
        }

        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        var expirationInHours = rememberMe
            ? jwtSettings.ExpirationInHoursRememberMe
            : jwtSettings.ExpirationInHoursDefault;

        var tokenDescriptor = BuildToken(user, roles, key, expirationInHours);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return new LoginResponseDto(jwt, false);
    }

    public async Task<string> RegisterUserAsync(string email, string password, string firstName, string lastName)
    {
        var user = new User
        {
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };

        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"User registration failed: {errors}");
        }

        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);
        var tokenDescriptor = BuildToken(user, roles, key, jwtSettings.ExpirationInHoursDefault);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }

    private SecurityTokenDescriptor BuildToken(User user, IList<string> roles, byte[] key, int expirationInHours) => new()
    {
        Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(Claims.FirstName, user.FirstName),
                new Claim(Claims.LastName, user.LastName),
                .. roles.Select(role => new Claim(ClaimTypes.Role, role))
                ]),
        Expires = DateTime.UtcNow.AddHours(expirationInHours),
        Issuer = jwtSettings.Issuer,
        Audience = jwtSettings.Audience,
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    public async Task ConfirmEmailAsync(string userId, string code)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException("User not found.");

        var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        var result = await userManager.ConfirmEmailAsync(user, decodedCode);

        if (!result.Succeeded)
            throw new InvalidOperationException("Failed to confirm email.");
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return null;

        var passwordResetToken = await userManager.GeneratePasswordResetTokenAsync(user);
        passwordResetToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(passwordResetToken));

        return passwordResetToken;
    }

    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await userManager.FindByEmailAsync(email)
            ?? throw new InvalidOperationException("User not found.");

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        var result = await userManager.ResetPasswordAsync(user, decodedToken, newPassword);

        if (!result.Succeeded)
            throw new InvalidOperationException("Failed to reset password.");
    }

    public async Task UpdateProfileAsync(string userId, string firstName, string lastName)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException("User not found.");

        user.FirstName = firstName;
        user.LastName = lastName;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
            throw new InvalidOperationException("Failed to update user profile.");
    }

    public async Task<TwoFactorAuthenticationDto> Generate2faKeyAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException("User not found.");

        var unformattedKey = await userManager.GetAuthenticatorKeyAsync(user);
        if (string.IsNullOrEmpty(unformattedKey))
        {
            await userManager.ResetAuthenticatorKeyAsync(user);
            unformattedKey = await userManager.GetAuthenticatorKeyAsync(user);
        }

        var sharedKey = FormatKey(unformattedKey);
        var authenticatorUri = $"otpauth://totp/GroovE:{user.Email}?secret={sharedKey}&issuer=GroovE&digits=6";

        string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            var currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition, 4));
                result.Append(' ');
                currentPosition += 4;
            }

            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.AsSpan(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        return new TwoFactorAuthenticationDto(sharedKey, authenticatorUri);
    }

    public async Task Enable2faAsync(string userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException("User not found.");

        var verificationCode = token.Replace(" ", string.Empty).Replace("-", string.Empty);

        var is2faTokenValid = await userManager.VerifyTwoFactorTokenAsync(
            user, userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

        if (!is2faTokenValid)
            throw new InvalidOperationException("Invalid token.");

        await userManager.SetTwoFactorEnabledAsync(user, true);
    }

    public async Task<string> LoginWith2faAsync(string email, string twoFactorCode)
    {
        var user = await userManager.FindByEmailAsync(email)
            ?? throw new InvalidOperationException("User not found.");

        var result = await signInManager.TwoFactorAuthenticatorSignInAsync(twoFactorCode, false, false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException("Invalid 2FA code.");

        var roles = await userManager.GetRolesAsync(user);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                .. roles.Select(role => new Claim(ClaimTypes.Role, role))
            ]),
            Expires = DateTime.UtcNow.AddHours(jwtSettings.ExpirationInHoursDefault),
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }

    public async Task Disable2faAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException("User not found.");

        await userManager.SetTwoFactorEnabledAsync(user, false);
    }

    public async Task<string> GenerateEmailConfirmationLinkAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email)
            ?? throw new InvalidOperationException("User not found.");

        var emailConfirmationCode = await userManager.GenerateEmailConfirmationTokenAsync(user)
                    ?? throw new InvalidOperationException("Failed to generate email confirmation token.");

        emailConfirmationCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmationCode));
        var confirmationLink = $"/api/identity/confirmEmail?code={emailConfirmationCode}&userId={user.Id}";

        return confirmationLink;
    }

    public async Task<UserProfileDto> GetProfileAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException("User not found.");

        return new UserProfileDto(user.Id, user.FirstName, user.LastName, user.Email);
    }

    public async Task<string> GetUserIdByEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email)
            ?? throw new InvalidOperationException("User not found.");

        return user.Id;
    }
}
