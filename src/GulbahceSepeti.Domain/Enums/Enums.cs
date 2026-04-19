namespace GulbahceSepeti.Domain.Enums;

public enum OrderStatus
{
    Pending = 0,
    Confirmed = 1,
    Preparing = 2,
    ReadyForDelivery = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Cancelled = 6
}

public enum PaymentMethod
{
    Cash = 0,
    CreditCard = 1,
    BankCard = 2,
    Pluxee = 3,
    Multinet = 4,
    Setcard = 5,
    Tokenflex = 6
}

public enum DeliveryZone
{
    IyteCampus = 0,
    GulbahceVillage = 1
}

public enum RestaurantStatus
{
    Pending = 0,
    Active = 1,
    Suspended = 2,
    Rejected = 3
}
