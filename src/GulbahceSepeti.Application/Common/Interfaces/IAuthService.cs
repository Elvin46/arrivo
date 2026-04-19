using GulbahceSepeti.Application.Features.Auth.DTOs;

namespace GulbahceSepeti.Application.Common.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<UserProfileDto> GetProfileAsync(string userId, CancellationToken ct = default);
}
