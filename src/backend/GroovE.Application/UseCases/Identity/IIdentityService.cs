using GroovE.Application.UseCases.Identity.Dtos;

namespace GroovE.Application.UseCases.Identity;

public interface IIdentityService
{
    Task<LoginResponseDto> LoginUserAsync(string email, string password, bool rememberMe);
    Task<string> RegisterUserAsync(string email, string password, string firstName, string lastName);
    Task<string> GenerateEmailConfirmationLinkAsync(string email);
    Task ConfirmEmailAsync(string userId, string code);
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task ResetPasswordAsync(string email, string token, string newPassword);
    Task<UserProfileDto> GetProfileAsync(string userId);
    Task UpdateProfileAsync(string userId, string firstName, string lastName);
    Task<TwoFactorAuthenticationDto> Generate2faKeyAsync(string userId);
    Task Enable2faAsync(string userId, string token);
    Task<string> LoginWith2faAsync(string twoFactorToken, string twoFactorCode, bool rememberMe);
    Task Disable2faAsync(string userId);
    Task<string> GetUserIdByEmail(string email);
}
