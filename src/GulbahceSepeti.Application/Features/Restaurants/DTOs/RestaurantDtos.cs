using GulbahceSepeti.Domain.Enums;

namespace GulbahceSepeti.Application.Features.Restaurants.DTOs;

public record RestaurantDto(
    Guid Id,
    string Name,
    string Description,
    string PhoneNumber,
    string Address,
    string? LogoUrl,
    string OpeningTime,
    string ClosingTime,
    decimal MinimumOrderAmount,
    int EstimatedDeliveryMinutes,
    RestaurantStatus Status,
    bool IsOpen
);

public record RestaurantListDto(
    Guid Id,
    string Name,
    string? LogoUrl,
    decimal MinimumOrderAmount,
    int EstimatedDeliveryMinutes,
    bool IsOpen
);

public record CreateRestaurantRequest(
    string Name,
    string Description,
    string PhoneNumber,
    string Address,
    string OpeningTime,   // "09:00"
    string ClosingTime,   // "23:00"
    decimal MinimumOrderAmount
);

public record UpdateRestaurantRequest(
    string Name,
    string Description,
    string PhoneNumber,
    string OpeningTime,
    string ClosingTime,
    decimal MinimumOrderAmount,
    int EstimatedDeliveryMinutes
);
