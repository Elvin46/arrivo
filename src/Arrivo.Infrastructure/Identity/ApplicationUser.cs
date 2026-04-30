using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Arrivo.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}

public static class AppRoles
{
    public const string Customer = "customer";
    public const string Cashier = "cashier";
    public const string RestaurantOwner = "restaurant_owner";
    public const string PlatformAdmin = "platform_admin";

    public static readonly string[] All = [Customer, Cashier, RestaurantOwner, PlatformAdmin];
}
