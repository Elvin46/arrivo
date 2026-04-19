namespace Arrivo.Application.Common.Interfaces;

public interface IOrderHubService
{
    /// <summary>
    /// Yeni sipariş geldiğinde restoran kasasına bildirim gönderir.
    /// </summary>
    Task NotifyNewOrderAsync(Guid restaurantId, object orderPayload);

    /// <summary>
    /// Sipariş durumu değiştiğinde müşteriye bildirim gönderir.
    /// </summary>
    Task NotifyOrderStatusChangedAsync(string customerId, Guid orderId, string status);
}
