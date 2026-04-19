using Arrivo.Application.Common.Exceptions;
using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.Orders.DTOs;
using Arrivo.Domain.Entities;
using Arrivo.Domain.Enums;
using Arrivo.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Arrivo.Infrastructure.Services;

public class OrderService : IOrderService
{
    private readonly IApplicationDbContext _db;
    private readonly IOrderHubService _hub;

    public OrderService(IApplicationDbContext db, IOrderHubService hub)
    {
        _db = db;
        _hub = hub;
    }

    public async Task<OrderDto> PlaceOrderAsync(PlaceOrderRequest request, string customerId, CancellationToken ct = default)
    {
        var restaurant = await _db.Restaurants.FindAsync([request.RestaurantId], ct)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId);

        if (!restaurant.IsOpen())
            throw new BusinessRuleException("Restoran şu anda hizmet vermemektedir.");

        // Validate and snapshot menu items
        var menuItemIds = request.Items.Select(i => i.MenuItemId).ToList();
        var menuItems = await _db.MenuItems
            .Where(m => menuItemIds.Contains(m.Id) && m.RestaurantId == request.RestaurantId && m.IsAvailable)
            .ToListAsync(ct);

        if (menuItems.Count != menuItemIds.Distinct().Count())
            throw new BusinessRuleException("Bazı ürünler mevcut değil veya sipariş verilemez.");

        var order = Order.Create(
            customerId, request.RestaurantId, request.PaymentMethod,
            request.DeliveryZone, request.DeliveryAddress, request.DeliveryBuilding,
            request.CustomerNote, request.CashChangeFor
        );

        foreach (var itemReq in request.Items)
        {
            var menuItem = menuItems.First(m => m.Id == itemReq.MenuItemId);
            var orderItem = OrderItem.Create(
                order.Id, menuItem.Id, menuItem.Name, menuItem.Price, itemReq.Quantity, itemReq.Note
            );
            order.AddItem(orderItem);
        }

        if (order.TotalAmount < restaurant.MinimumOrderAmount)
            throw new BusinessRuleException($"Minimum sipariş tutarı {restaurant.MinimumOrderAmount:C} olmalıdır.");

        _db.Orders.Add(order);
        await _db.SaveChangesAsync(ct);

        // SignalR — kasaya bildir
        var dto = await BuildOrderDtoAsync(order.Id, ct);
        await _hub.NotifyNewOrderAsync(restaurant.Id, dto);

        return dto;
    }

    public async Task<OrderDto> GetByIdAsync(Guid id, string requestingUserId, CancellationToken ct = default)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .FirstOrDefaultAsync(o => o.Id == id, ct)
            ?? throw new NotFoundException(nameof(Order), id);

        // Müşteri sadece kendi siparişini görebilir (admin/cashier/owner her şeyi görebilir — controller'da rol kontrolü)
        return MapToDto(order);
    }

    public async Task<IEnumerable<OrderSummaryDto>> GetOrdersAsync(string requestingUserId, Guid? restaurantId, CancellationToken ct = default)
    {
        var query = _db.Orders
            .Include(o => o.Restaurant)
            .AsQueryable();

        if (restaurantId.HasValue)
            query = query.Where(o => o.RestaurantId == restaurantId.Value);
        else
            query = query.Where(o => o.CustomerId == requestingUserId);

        var orders = await query
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(ct);

        return orders.Select(o => new OrderSummaryDto(
            o.Id, o.Restaurant.Name, o.Status, o.TotalAmount, o.CreatedAt
        ));
    }

    public async Task<OrderDto> UpdateStatusAsync(Guid id, UpdateOrderStatusRequest request, string requestingUserId, CancellationToken ct = default)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .FirstOrDefaultAsync(o => o.Id == id, ct)
            ?? throw new NotFoundException(nameof(Order), id);

        order.UpdateStatus(request.NewStatus);

        if (request.EstimatedDeliveryMinutes.HasValue)
            order.SetEstimatedDelivery(request.EstimatedDeliveryMinutes.Value);

        await _db.SaveChangesAsync(ct);

        // SignalR — müşteriye bildir
        await _hub.NotifyOrderStatusChangedAsync(order.CustomerId, order.Id, request.NewStatus.ToString());

        return MapToDto(order);
    }

    // ─── Helpers ────────────────────────────────────────────────────────────────

    private async Task<OrderDto> BuildOrderDtoAsync(Guid orderId, CancellationToken ct)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .Include(o => o.Restaurant)
            .FirstAsync(o => o.Id == orderId, ct);
        return MapToDto(order);
    }

    private static OrderDto MapToDto(Order o) => new(
        o.Id, o.RestaurantId, o.Restaurant.Name, o.Status, o.PaymentMethod,
        o.DeliveryZone, o.DeliveryAddress, o.DeliveryBuilding,
        o.TotalAmount, o.CustomerNote, o.EstimatedDeliveryTime, o.CreatedAt,
        o.Items.Select(i => new OrderItemDto(i.MenuItemId, i.MenuItemName, i.UnitPrice, i.Quantity, i.Note)).ToList()
    );
}
