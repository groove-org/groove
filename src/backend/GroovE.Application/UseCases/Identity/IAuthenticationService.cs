namespace GroovE.Application.UseCases.Identity;

public interface IAuthenticationService
{
    Task<LoginResponseDto> LoginUser(string email, string password, bool rememberMe);
    Task<string> RegisterUser(string email, string password, string firstName, string lastName);
    Task ConfirmEmailAsync(string userId, string code);
    Task<PasswordResetTokenDto?> GeneratePasswordResetTokenAsync(string email);
    Task ResetPasswordAsync(string email, string token, string newPassword);
    Task<UserProfileDto> GetProfileAsync(string userId);
    Task UpdateProfileAsync(string userId, string firstName, string lastName);
    Task<TwoFactorAuthenticationDto> Generate2faKeyAsync(string userId);
    Task Enable2faAsync(string userId, string token);
    Task<string> LoginWith2faAsync(string email, string twoFactorCode);
    Task Disable2faAsync(string userId);
    Task ResendConfirmationEmailAsync(string email);
}
