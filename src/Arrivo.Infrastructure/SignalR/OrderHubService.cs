using Arrivo.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Arrivo.Infrastructure.SignalR;

public class OrderHubService : IOrderHubService
{
    private readonly IHubContext<OrderHub> _hub;

    public OrderHubService(IHubContext<OrderHub> hub) => _hub = hub;

    public async Task NotifyNewOrderAsync(Guid restaurantId, object orderPayload)
    {
        await _hub.Clients
            .Group($"restaurant-{restaurantId}")
            .SendAsync("NewOrder", orderPayload);
    }

    public async Task NotifyOrderStatusChangedAsync(string customerId, Guid orderId, string status)
    {
        await _hub.Clients
            .Group($"customer-{customerId}")
            .SendAsync("OrderStatusChanged", new { orderId, status });
    }
}
