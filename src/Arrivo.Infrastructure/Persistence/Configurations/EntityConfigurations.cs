using Arrivo.Domain.Entities;
using Arrivo.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arrivo.Infrastructure.Persistence.Configurations;

public class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).HasMaxLength(100).IsRequired();
        builder.Property(r => r.Description).HasMaxLength(500);
        builder.Property(r => r.PhoneNumber).HasMaxLength(20).IsRequired();
        builder.Property(r => r.Address).HasMaxLength(300).IsRequired();
        builder.Property(r => r.MinimumOrderAmount).HasColumnType("decimal(10,2)");
        builder.Property(r => r.OwnerId).IsRequired();

        builder.HasIndex(r => r.OwnerId);

        builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(r => r.OwnerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(r => r.MenuItems)
               .WithOne(m => m.Restaurant)
               .HasForeignKey(m => m.RestaurantId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.Orders)
               .WithOne(o => o.Restaurant)
               .HasForeignKey(o => o.RestaurantId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
        builder.Property(m => m.Description).HasMaxLength(500);
        builder.Property(m => m.Category).HasMaxLength(60).IsRequired();
        builder.Property(m => m.ImageUrl).HasMaxLength(500);
        builder.Property(m => m.Price).HasColumnType("decimal(10,2)");

        builder.HasIndex(m => new { m.RestaurantId, m.Category });
    }
}

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.DeliveryAddress).HasMaxLength(300).IsRequired();
        builder.Property(o => o.DeliveryBuilding).HasMaxLength(100);
        builder.Property(o => o.CustomerNote).HasMaxLength(500);
        builder.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");
        builder.Property(o => o.CashChangeFor).HasColumnType("decimal(10,2)");
        builder.Property(o => o.CustomerId).IsRequired();

        builder.HasIndex(o => o.CustomerId);
        builder.HasIndex(o => new { o.RestaurantId, o.Status });

        builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(o => o.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.Items)
               .WithOne(i => i.Order)
               .HasForeignKey(i => i.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.MenuItemName).HasMaxLength(100).IsRequired();
        builder.Property(i => i.UnitPrice).HasColumnType("decimal(10,2)");
        builder.Property(i => i.Note).HasMaxLength(300);

        builder.HasOne(i => i.MenuItem)
               .WithMany(m => m.OrderItems)
               .HasForeignKey(i => i.MenuItemId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}

public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Title).HasMaxLength(50).IsRequired();
        builder.Property(a => a.AddressLine).HasMaxLength(300).IsRequired();
        builder.Property(a => a.Building).HasMaxLength(100);
        builder.Property(a => a.UserId).IsRequired();

        builder.HasIndex(a => a.UserId);

        builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
