using GulbahceSepeti.Domain.Enums;

namespace GulbahceSepeti.Application.Features.Orders.DTOs;

public record PlaceOrderRequest(
    Guid RestaurantId,
    PaymentMethod PaymentMethod,
    DeliveryZone DeliveryZone,
    string DeliveryAddress,
    string? DeliveryBuilding,
    string? CustomerNote,
    decimal? CashChangeFor,
    List<OrderItemRequest> Items
);

public record OrderItemRequest(
    Guid MenuItemId,
    int Quantity,
    string? Note
);

public record OrderDto(
    Guid Id,
    Guid RestaurantId,
    string RestaurantName,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    DeliveryZone DeliveryZone,
    string DeliveryAddress,
    string? DeliveryBuilding,
    decimal TotalAmount,
    string? CustomerNote,
    DateTime? EstimatedDeliveryTime,
    DateTime CreatedAt,
    List<OrderItemDto> Items
);

public record OrderItemDto(
    Guid MenuItemId,
    string MenuItemName,
    decimal UnitPrice,
    int Quantity,
    string? Note
);

public record UpdateOrderStatusRequest(
    OrderStatus NewStatus,
    int? EstimatedDeliveryMinutes
);

public record OrderSummaryDto(
    Guid Id,
    string RestaurantName,
    OrderStatus Status,
    decimal TotalAmount,
    DateTime CreatedAt
);
