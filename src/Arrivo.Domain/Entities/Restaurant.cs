using Arrivo.Domain.Common;
using Arrivo.Domain.Enums;

namespace Arrivo.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string? LogoUrl { get; private set; }
    public TimeOnly OpeningTime { get; private set; }
    public TimeOnly ClosingTime { get; private set; }
    public decimal MinimumOrderAmount { get; private set; }
    public int EstimatedDeliveryMinutes { get; private set; } = 30;
    public RestaurantStatus Status { get; private set; } = RestaurantStatus.Pending;
    public string OwnerId { get; private set; } = default!;

    // Navigation
    public ICollection<MenuItem> MenuItems { get; private set; } = new List<MenuItem>();
    public ICollection<Order> Orders { get; private set; } = new List<Order>();

    protected Restaurant() { }

    public static Restaurant Create(
        string name,
        string description,
        string phoneNumber,
        string address,
        TimeOnly openingTime,
        TimeOnly closingTime,
        decimal minimumOrderAmount,
        string ownerId)
    {
        return new Restaurant
        {
            Name = name,
            Description = description,
            PhoneNumber = phoneNumber,
            Address = address,
            OpeningTime = openingTime,
            ClosingTime = closingTime,
            MinimumOrderAmount = minimumOrderAmount,
            OwnerId = ownerId
        };
    }

    public void Approve() => Status = RestaurantStatus.Active;
    public void Reject() => Status = RestaurantStatus.Rejected;
    public void Suspend() => Status = RestaurantStatus.Suspended;

    public bool IsOpen()
    {
        var now = TimeOnly.FromDateTime(DateTime.Now);
        return Status == RestaurantStatus.Active &&
               now >= OpeningTime &&
               now <= ClosingTime;
    }

    public void Update(string name, string description, string phoneNumber,
        TimeOnly openingTime, TimeOnly closingTime, decimal minimumOrderAmount,
        int estimatedDeliveryMinutes)
    {
        Name = name;
        Description = description;
        PhoneNumber = phoneNumber;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        MinimumOrderAmount = minimumOrderAmount;
        EstimatedDeliveryMinutes = estimatedDeliveryMinutes;
        SetUpdatedAt();
    }
}
