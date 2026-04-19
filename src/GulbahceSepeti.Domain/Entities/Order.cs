using GulbahceSepeti.Domain.Common;
using GulbahceSepeti.Domain.Enums;

namespace GulbahceSepeti.Domain.Entities;

public class Order : BaseEntity
{
    public string CustomerId { get; private set; } = default!;
    public Guid RestaurantId { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public PaymentMethod PaymentMethod { get; private set; }
    public DeliveryZone DeliveryZone { get; private set; }
    public string DeliveryAddress { get; private set; } = default!;
    public string? DeliveryBuilding { get; private set; }   // İYTE bina/fakülte
    public decimal TotalAmount { get; private set; }
    public decimal? CashChangeFor { get; private set; }      // Nakit para üstü için
    public string? CustomerNote { get; private set; }
    public DateTime? EstimatedDeliveryTime { get; private set; }

    // Navigation
    public Restaurant Restaurant { get; private set; } = default!;
    public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

    protected Order() { }

    public static Order Create(
        string customerId,
        Guid restaurantId,
        PaymentMethod paymentMethod,
        DeliveryZone deliveryZone,
        string deliveryAddress,
        string? deliveryBuilding,
        string? customerNote,
        decimal? cashChangeFor = null)
    {
        return new Order
        {
            CustomerId = customerId,
            RestaurantId = restaurantId,
            PaymentMethod = paymentMethod,
            DeliveryZone = deliveryZone,
            DeliveryAddress = deliveryAddress,
            DeliveryBuilding = deliveryBuilding,
            CustomerNote = customerNote,
            CashChangeFor = cashChangeFor
        };
    }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
        RecalculateTotal();
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        Status = newStatus;
        SetUpdatedAt();
    }

    public void SetEstimatedDelivery(int minutes)
    {
        EstimatedDeliveryTime = DateTime.UtcNow.AddMinutes(minutes);
        SetUpdatedAt();
    }

    private void RecalculateTotal()
    {
        TotalAmount = Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Guid MenuItemId { get; private set; }
    public string MenuItemName { get; private set; } = default!;  // snapshot
    public decimal UnitPrice { get; private set; }                 // snapshot
    public int Quantity { get; private set; }
    public string? Note { get; private set; }

    // Navigation
    public Order Order { get; private set; } = default!;
    public MenuItem MenuItem { get; private set; } = default!;

    protected OrderItem() { }

    public static OrderItem Create(Guid orderId, Guid menuItemId,
        string menuItemName, decimal unitPrice, int quantity, string? note = null)
    {
        return new OrderItem
        {
            OrderId = orderId,
            MenuItemId = menuItemId,
            MenuItemName = menuItemName,
            UnitPrice = unitPrice,
            Quantity = quantity,
            Note = note
        };
    }
}
