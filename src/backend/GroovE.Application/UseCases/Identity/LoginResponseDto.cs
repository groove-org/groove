namespace GroovE.Application.UseCases.Identity;

public record LoginResponseDto(string? Token, bool RequiresTwoFactor);
