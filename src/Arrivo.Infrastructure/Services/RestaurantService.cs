using Arrivo.Application.Common.Exceptions;
using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.Restaurants.DTOs;
using Arrivo.Domain.Entities;
using Arrivo.Domain.Enums;
using Arrivo.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Arrivo.Infrastructure.Services;

public class RestaurantService : IRestaurantService
{
    private readonly IApplicationDbContext _db;

    public RestaurantService(IApplicationDbContext db) => _db = db;

    public async Task<IEnumerable<RestaurantListDto>> GetActiveRestaurantsAsync(CancellationToken ct = default)
    {
        var restaurants = await _db.Restaurants
            .Where(r => r.Status == RestaurantStatus.Active)
            .ToListAsync(ct);

        return restaurants.Select(r => new RestaurantListDto(
            r.Id, r.Name, r.LogoUrl, r.MinimumOrderAmount, r.EstimatedDeliveryMinutes, r.IsOpen()
        ));
    }

    public async Task<RestaurantDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var r = await _db.Restaurants.FindAsync([id], ct)
            ?? throw new NotFoundException(nameof(Restaurant), id);
        return MapToDto(r);
    }

    public async Task<RestaurantDto> CreateAsync(CreateRestaurantRequest request, string ownerId, CancellationToken ct = default)
    {
        var restaurant = Restaurant.Create(
            request.Name, request.Description, request.PhoneNumber,
            request.Address,
            TimeOnly.Parse(request.OpeningTime),
            TimeOnly.Parse(request.ClosingTime),
            request.MinimumOrderAmount,
            ownerId
        );

        _db.Restaurants.Add(restaurant);
        await _db.SaveChangesAsync(ct);
        return MapToDto(restaurant);
    }

    public async Task<RestaurantDto> UpdateAsync(Guid id, UpdateRestaurantRequest request, string requestingUserId, bool isAdmin = false, CancellationToken ct = default)
    {
        var r = await _db.Restaurants.FindAsync([id], ct)
            ?? throw new NotFoundException(nameof(Restaurant), id);

        if (!isAdmin && r.OwnerId != requestingUserId)
            throw new ForbiddenException();

        r.Update(request.Name, request.Description, request.PhoneNumber,
            TimeOnly.Parse(request.OpeningTime), TimeOnly.Parse(request.ClosingTime),
            request.MinimumOrderAmount, request.EstimatedDeliveryMinutes);

        await _db.SaveChangesAsync(ct);
        return MapToDto(r);
    }

    public async Task SetStatusAsync(Guid id, bool activate, CancellationToken ct = default)
    {
        var r = await _db.Restaurants.FindAsync([id], ct)
            ?? throw new NotFoundException(nameof(Restaurant), id);

        if (activate)
            r.Approve();
        else
            r.Suspend();

        await _db.SaveChangesAsync(ct);
    }

    public async Task ApproveAsync(Guid id, CancellationToken ct = default)
    {
        var r = await _db.Restaurants.FindAsync([id], ct)
            ?? throw new NotFoundException(nameof(Restaurant), id);
        r.Approve();
        await _db.SaveChangesAsync(ct);
    }

    public async Task RejectAsync(Guid id, CancellationToken ct = default)
    {
        var r = await _db.Restaurants.FindAsync([id], ct)
            ?? throw new NotFoundException(nameof(Restaurant), id);
        r.Reject();
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IEnumerable<RestaurantDto>> GetPendingAsync(CancellationToken ct = default)
    {
        var list = await _db.Restaurants
            .Where(r => r.Status == RestaurantStatus.Pending)
            .ToListAsync(ct);
        return list.Select(MapToDto);
    }

    private static RestaurantDto MapToDto(Restaurant r) => new(
        r.Id, r.Name, r.Description, r.PhoneNumber, r.Address, r.LogoUrl,
        r.OpeningTime.ToString("HH:mm"), r.ClosingTime.ToString("HH:mm"),
        r.MinimumOrderAmount, r.EstimatedDeliveryMinutes, r.Status, r.IsOpen()
    );
}
