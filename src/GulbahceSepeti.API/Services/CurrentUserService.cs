using System.Security.Claims;
using GulbahceSepeti.Application.Common.Interfaces;

namespace GulbahceSepeti.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUserService(IHttpContextAccessor accessor) => _accessor = accessor;

    public string? UserId =>
        _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName =>
        _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

    public IEnumerable<string> Roles =>
        _accessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(c => c.Value)
        ?? Enumerable.Empty<string>();

    public bool IsInRole(string role) =>
        _accessor.HttpContext?.User.IsInRole(role) ?? false;
}
