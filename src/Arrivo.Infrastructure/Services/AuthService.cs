using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Arrivo.Application.Common.Exceptions;
using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.Auth.DTOs;
using Arrivo.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Arrivo.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
    {
        var existing = await _userManager.FindByNameAsync(request.PhoneNumber);
        if (existing != null)
            throw new BusinessRuleException("Bu telefon numarası zaten kayıtlı.");

        var user = new ApplicationUser
        {
            UserName = request.PhoneNumber,
            PhoneNumber = request.PhoneNumber,
            FullName = request.FullName,
            PhoneNumberConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new BusinessRuleException(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, AppRoles.Customer);

        return await BuildAuthResponseAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userManager.FindByNameAsync(request.PhoneNumber)
            ?? throw new BusinessRuleException("Telefon numarası veya şifre hatalı.");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new BusinessRuleException("Telefon numarası veya şifre hatalı.");

        if (!user.IsActive)
            throw new BusinessRuleException("Hesabınız askıya alınmıştır.");

        return await BuildAuthResponseAsync(user);
    }

    public async Task<UserProfileDto> GetProfileAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(ApplicationUser), userId);

        var roles = await _userManager.GetRolesAsync(user);
        return new UserProfileDto(user.Id, user.FullName, user.PhoneNumber!, roles);
    }

    // ─── Private ────────────────────────────────────────────────────────────────

    private async Task<AuthResponse> BuildAuthResponseAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var (token, expires) = GenerateJwt(user, roles);

        return new AuthResponse(user.Id, user.FullName, user.PhoneNumber!, token, expires, roles);
    }

    private (string Token, DateTime Expires) GenerateJwt(ApplicationUser user, IList<string> roles)
    {
        var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddDays(7);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.FullName),
            new("phone", user.PhoneNumber ?? ""),
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}
