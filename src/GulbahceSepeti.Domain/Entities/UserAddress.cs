using GulbahceSepeti.Domain.Common;
using GulbahceSepeti.Domain.Enums;

namespace GulbahceSepeti.Domain.Entities;

public class UserAddress : BaseEntity
{
    public string UserId { get; private set; } = default!;
    public string Title { get; private set; } = default!;       // "Ev", "İş", "Yurt"
    public DeliveryZone Zone { get; private set; }
    public string AddressLine { get; private set; } = default!;
    public string? Building { get; private set; }                // İYTE için
    public bool IsDefault { get; private set; }

    protected UserAddress() { }

    public static UserAddress Create(string userId, string title, DeliveryZone zone,
        string addressLine, string? building = null, bool isDefault = false)
    {
        return new UserAddress
        {
            UserId = userId,
            Title = title,
            Zone = zone,
            AddressLine = addressLine,
            Building = building,
            IsDefault = isDefault
        };
    }

    public void SetDefault(bool isDefault) => IsDefault = isDefault;
}
