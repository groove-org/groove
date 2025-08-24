using System.Security.Claims;
using GroovE.Application.Common;
using GroovE.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GroovE.Api.Common;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string? Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? FirstName => httpContextAccessor.HttpContext?.User.FindFirstValue("FirstName");

    public string? LastName => httpContextAccessor.HttpContext?.User.FindFirstValue("LastName");

    public string? Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
}
