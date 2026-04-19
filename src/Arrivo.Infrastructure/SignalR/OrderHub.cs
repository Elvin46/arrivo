using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Arrivo.Infrastructure.SignalR;

[Authorize]
public class OrderHub : Hub
{
    /// <summary>
    /// Kasiyer/restoran sahibi kasayı açtığında kendi restoran grubuna katılır.
    /// </summary>
    public async Task JoinRestaurantGroup(string restaurantId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"restaurant-{restaurantId}");
    }

    public async Task LeaveRestaurantGroup(string restaurantId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"restaurant-{restaurantId}");
    }

    /// <summary>
    /// Müşteri sipariş takibi için kendi grubuna katılır.
    /// </summary>
    public async Task JoinCustomerGroup(string userId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"customer-{userId}");
    }
}
