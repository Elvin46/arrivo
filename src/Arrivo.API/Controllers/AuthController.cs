using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.Auth.DTOs;
using Arrivo.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arrivo.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth) => _auth = auth;

    /// <summary>Yeni kullanıcı kaydı (e-posta ile)</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        var result = await _auth.RegisterAsync(request, ct);
        return CreatedAtAction(nameof(Me), result);
    }

    /// <summary>Giriş — JWT access token + refresh token döner</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await _auth.LoginAsync(request, ct);
        return Ok(result);
    }

    /// <summary>Refresh token ile yeni access token alır</summary>
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request, CancellationToken ct)
    {
        var result = await _auth.RefreshTokenAsync(request.RefreshToken, ct);
        return Ok(result);
    }

    /// <summary>Token'dan profil bilgisi döner</summary>
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> Me(CancellationToken ct)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value;
        var profile = await _auth.GetProfileAsync(userId, ct);
        return Ok(profile);
    }

    /// <summary>Platform admin testi — sadece platform_admin rolü erişebilir</summary>
    [HttpGet("admin-only")]
    [Authorize(Roles = AppRoles.PlatformAdmin)]
    public IActionResult AdminOnly()
    {
        return Ok(new { message = "Platform admin erişimi başarılı.", userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value });
    }
}
