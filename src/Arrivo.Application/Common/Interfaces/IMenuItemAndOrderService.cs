using Arrivo.Application.Features.MenuItems.DTOs;
using Arrivo.Application.Features.Orders.DTOs;

namespace Arrivo.Application.Common.Interfaces;

public interface IMenuItemService
{
    Task<IEnumerable<MenuItemDto>> GetByRestaurantAsync(Guid restaurantId, string? category = null, CancellationToken ct = default);
    Task<MenuItemDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<MenuItemDto> CreateAsync(CreateMenuItemRequest request, string requestingUserId, CancellationToken ct = default);
    Task<MenuItemDto> UpdateAsync(Guid id, UpdateMenuItemRequest request, string requestingUserId, CancellationToken ct = default);
    Task SetAvailabilityAsync(Guid id, bool isAvailable, string requestingUserId, bool skipOwnerCheck = false, CancellationToken ct = default);
    Task DeleteAsync(Guid id, string requestingUserId, CancellationToken ct = default);
}

public interface IOrderService
{
    Task<OrderDto> PlaceOrderAsync(PlaceOrderRequest request, string customerId, CancellationToken ct = default);
    Task<OrderDto> GetByIdAsync(Guid id, string requestingUserId, CancellationToken ct = default);
    Task<IEnumerable<OrderSummaryDto>> GetOrdersAsync(string requestingUserId, Guid? restaurantId, CancellationToken ct = default);
    Task<OrderDto> UpdateStatusAsync(Guid id, UpdateOrderStatusRequest request, string requestingUserId, CancellationToken ct = default);
}
