using GulbahceSepeti.Domain.Common;

namespace GulbahceSepeti.Domain.Entities;

public class MenuItem : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public string? ImageUrl { get; private set; }
    public string Category { get; private set; } = default!;
    public bool IsAvailable { get; private set; } = true;
    public Guid RestaurantId { get; private set; }

    // Navigation
    public Restaurant Restaurant { get; private set; } = default!;
    public ICollection<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

    protected MenuItem() { }

    public static MenuItem Create(string name, string description, decimal price,
        string category, Guid restaurantId, string? imageUrl = null)
    {
        return new MenuItem
        {
            Name = name,
            Description = description,
            Price = price,
            Category = category,
            RestaurantId = restaurantId,
            ImageUrl = imageUrl
        };
    }

    public void Update(string name, string description, decimal price,
        string category, string? imageUrl)
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;
        ImageUrl = imageUrl;
        SetUpdatedAt();
    }

    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
        SetUpdatedAt();
    }
}
