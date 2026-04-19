namespace GulbahceSepeti.Application.Features.MenuItems.DTOs;

public record MenuItemDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    string Category,
    bool IsAvailable,
    Guid RestaurantId
);

public record CreateMenuItemRequest(
    string Name,
    string Description,
    decimal Price,
    string Category,
    string? ImageUrl,
    Guid RestaurantId
);

public record UpdateMenuItemRequest(
    string Name,
    string Description,
    decimal Price,
    string Category,
    string? ImageUrl
);
