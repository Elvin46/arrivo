using GulbahceSepeti.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GulbahceSepeti.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Restaurant> Restaurants { get; }
    DbSet<MenuItem> MenuItems { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<UserAddress> UserAddresses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
