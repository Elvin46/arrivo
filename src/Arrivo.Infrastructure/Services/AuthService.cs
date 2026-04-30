using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Arrivo.Application.Common.Exceptions;
using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.Auth.DTOs;
using Arrivo.Domain.Entities;
using Arrivo.Infrastructure.Identity;
using Arrivo.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Arrivo.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly AppDbContext _db;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IConfiguration config,
        AppDbContext db)
    {
        _userManager = userManager;
        _config = config;
        _db = db;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
    {
        var existing = await _userManager.FindByEmailAsync(request.Email);
        if (existing != null)
            throw new BusinessRuleException("Bu e-posta adresi zaten kayıtlı.");

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            PhoneNumberConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            throw new BusinessRuleException(string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, AppRoles.Customer);

        return await BuildAuthResponseAsync(user, ct);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email)
            ?? throw new BusinessRuleException("E-posta veya şifre hatalı.");

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new BusinessRuleException("E-posta veya şifre hatalı.");

        if (!user.IsActive)
            throw new BusinessRuleException("Hesabınız askıya alınmıştır.");

        return await BuildAuthResponseAsync(user, ct);
    }

    public async Task<AuthResponse> RefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    {
        var stored = await _db.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == refreshToken, ct)
            ?? throw new BusinessRuleException("Geçersiz refresh token.");

        if (stored.IsRevoked)
            throw new BusinessRuleException("Refresh token iptal edilmiş.");

        if (stored.ExpiresAt < DateTime.UtcNow)
            throw new BusinessRuleException("Refresh token süresi dolmuş.");

        var user = await _userManager.FindByIdAsync(stored.UserId)
            ?? throw new NotFoundException(nameof(ApplicationUser), stored.UserId);

        if (!user.IsActive)
            throw new BusinessRuleException("Hesabınız askıya alınmıştır.");

        // Revoke old token
        stored.IsRevoked = true;
        await _db.SaveChangesAsync(ct);

        return await BuildAuthResponseAsync(user, ct);
    }

    public async Task<UserProfileDto> GetProfileAsync(string userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new NotFoundException(nameof(ApplicationUser), userId);

        var roles = await _userManager.GetRolesAsync(user);
        return new UserProfileDto(user.Id, user.Email!, user.FirstName, user.LastName, roles);
    }

    // ─── Private ────────────────────────────────────────────────────────────────

    private async Task<AuthResponse> BuildAuthResponseAsync(ApplicationUser user, CancellationToken ct)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var (accessToken, expires) = GenerateJwt(user, roles);
        var refreshToken = await GenerateRefreshTokenAsync(user.Id, ct);
        var primaryRole = roles.FirstOrDefault() ?? AppRoles.Customer;

        return new AuthResponse(
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            ExpiresAt: expires,
            User: new UserDto(user.Id, user.Email!, user.FirstName, user.LastName, primaryRole)
        );
    }

    private (string Token, DateTime Expires) GenerateJwt(ApplicationUser user, IList<string> roles)
    {
        var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(1);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.FullName),
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

    private async Task<string> GenerateRefreshTokenAsync(string userId, CancellationToken ct)
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(tokenBytes);

        var refreshToken = new RefreshToken
        {
            UserId = userId,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddDays(30),
            CreatedAt = DateTime.UtcNow
        };

        _db.RefreshTokens.Add(refreshToken);
        await _db.SaveChangesAsync(ct);

        return token;
    }
}
