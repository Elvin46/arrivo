using System.ComponentModel.DataAnnotations;

namespace Arrivo.Application.Features.Auth.DTOs;

public record RegisterRequest(
    [Required, MaxLength(50)] string FirstName,
    [Required, MaxLength(50)] string LastName,
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    [Required] string PhoneNumber
);

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password
);

public record RefreshTokenRequest(
    [Required] string RefreshToken
);

public record AuthResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    UserDto User
);

public record UserDto(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    string Role
);

public record UserProfileDto(
    string Id,
    string Email,
    string FirstName,
    string LastName,
    IEnumerable<string> Roles
);
