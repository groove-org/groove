using GroovE.Application.Identity;
using System.Security.Claims;

namespace GroovE.Api.Common;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string? Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? FirstName => httpContextAccessor.HttpContext?.User.FindFirstValue(Claims.FirstName);

    public string? LastName => httpContextAccessor.HttpContext?.User.FindFirstValue(Claims.LastName);

    public string? Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}
