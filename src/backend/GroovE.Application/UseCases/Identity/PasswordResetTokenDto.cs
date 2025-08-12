namespace GroovE.Application.UseCases.Identity;

public record PasswordResetTokenDto(string Email, string FirstName, string Token);
