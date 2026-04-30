using Arrivo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arrivo.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Restaurant> Restaurants { get; }
    DbSet<MenuItem> MenuItems { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<UserAddress> UserAddresses { get; }
    DbSet<RefreshToken> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
