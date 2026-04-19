using GulbahceSepeti.Application.Common.Exceptions;
using GulbahceSepeti.Application.Common.Interfaces;
using GulbahceSepeti.Application.Features.MenuItems.DTOs;
using GulbahceSepeti.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GulbahceSepeti.Infrastructure.Services;

public class MenuItemService : IMenuItemService
{
    private readonly IApplicationDbContext _db;

    public MenuItemService(IApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<MenuItemDto>> GetByRestaurantAsync(Guid restaurantId, CancellationToken ct = default)
    {
        var items = await _db.MenuItems
            .Where(m => m.RestaurantId == restaurantId)
            .ToListAsync(ct);
        return items.Select(MapToDto);
    }

    public async Task<MenuItemDto> CreateAsync(CreateMenuItemRequest request, string requestingUserId, CancellationToken ct = default)
    {
        var restaurant = await _db.Restaurants.FindAsync([request.RestaurantId], ct)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId);

        if (restaurant.OwnerId != requestingUserId)
            throw new ForbiddenException();

        var item = MenuItem.Create(
            request.Name, request.Description, request.Price,
            request.Category, request.RestaurantId, request.ImageUrl
        );

        _db.MenuItems.Add(item);
        await _db.SaveChangesAsync(ct);
        return MapToDto(item);
    }

    public async Task<MenuItemDto> UpdateAsync(Guid id, UpdateMenuItemRequest request, string requestingUserId, CancellationToken ct = default)
    {
        var item = await _db.MenuItems
            .Include(m => m.Restaurant)
            .FirstOrDefaultAsync(m => m.Id == id, ct)
            ?? throw new NotFoundException(nameof(MenuItem), id);

        if (item.Restaurant.OwnerId != requestingUserId)
            throw new ForbiddenException();

        item.Update(request.Name, request.Description, request.Price, request.Category, request.ImageUrl);
        await _db.SaveChangesAsync(ct);
        return MapToDto(item);
    }

    public async Task SetAvailabilityAsync(Guid id, bool isAvailable, string requestingUserId, CancellationToken ct = default)
    {
        var item = await _db.MenuItems
            .Include(m => m.Restaurant)
            .FirstOrDefaultAsync(m => m.Id == id, ct)
            ?? throw new NotFoundException(nameof(MenuItem), id);

        if (item.Restaurant.OwnerId != requestingUserId)
            throw new ForbiddenException();

        item.SetAvailability(isAvailable);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, string requestingUserId, CancellationToken ct = default)
    {
        var item = await _db.MenuItems
            .Include(m => m.Restaurant)
            .FirstOrDefaultAsync(m => m.Id == id, ct)
            ?? throw new NotFoundException(nameof(MenuItem), id);

        if (item.Restaurant.OwnerId != requestingUserId)
            throw new ForbiddenException();

        _db.MenuItems.Remove(item);
        await _db.SaveChangesAsync(ct);
    }

    private static MenuItemDto MapToDto(MenuItem m) => new(
        m.Id, m.Name, m.Description, m.Price, m.ImageUrl, m.Category, m.IsAvailable, m.RestaurantId
    );
}
