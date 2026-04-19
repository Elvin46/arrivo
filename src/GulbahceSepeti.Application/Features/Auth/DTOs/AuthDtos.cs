namespace GulbahceSepeti.Application.Features.Auth.DTOs;

public record RegisterRequest(
    string FullName,
    string PhoneNumber,
    string Password
);

public record LoginRequest(
    string PhoneNumber,
    string Password
);

public record AuthResponse(
    string UserId,
    string FullName,
    string PhoneNumber,
    string Token,
    DateTime ExpiresAt,
    IEnumerable<string> Roles
);

public record UserProfileDto(
    string UserId,
    string FullName,
    string PhoneNumber,
    IEnumerable<string> Roles
);
