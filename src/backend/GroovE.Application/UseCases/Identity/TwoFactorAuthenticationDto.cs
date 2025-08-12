namespace GroovE.Application.UseCases.Identity;

public record TwoFactorAuthenticationDto(string SharedKey, string AuthenticatorUri);
