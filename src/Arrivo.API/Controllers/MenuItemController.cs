using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.MenuItems.DTOs;
using Arrivo.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arrivo.API.Controllers;

[ApiController]
public class MenuItemController : ControllerBase
{
    private readonly IMenuItemService _svc;
    private readonly ICurrentUserService _currentUser;

    public MenuItemController(IMenuItemService svc, ICurrentUserService currentUser)
    {
        _svc = svc;
        _currentUser = currentUser;
    }

    /// <summary>Restoranın menü öğelerini listele — opsiyonel category filtresi (public)</summary>
    [HttpGet("api/restaurants/{restaurantId:guid}/menu-items")]
    public async Task<IActionResult> GetByRestaurant(
        Guid restaurantId,
        [FromQuery] string? category,
        CancellationToken ct)
    {
        var result = await _svc.GetByRestaurantAsync(restaurantId, category, ct);
        return Ok(result);
    }

    /// <summary>Menü öğesi detayı (public)</summary>
    [HttpGet("api/menu-items/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _svc.GetByIdAsync(id, ct);
        return Ok(result);
    }

    /// <summary>Restorana yeni menü öğesi ekle</summary>
    [HttpPost("api/restaurants/{restaurantId:guid}/menu-items")]
    [Authorize(Roles = AppRoles.RestaurantOwner)]
    public async Task<IActionResult> Create(
        Guid restaurantId,
        [FromBody] CreateMenuItemRequest request,
        CancellationToken ct)
    {
        // Ensure the restaurantId in the route matches the request body
        var merged = request with { RestaurantId = restaurantId };
        var result = await _svc.CreateAsync(merged, _currentUser.UserId!, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Menü öğesini güncelle</summary>
    [HttpPut("api/menu-items/{id:guid}")]
    [Authorize(Roles = AppRoles.RestaurantOwner)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMenuItemRequest request, CancellationToken ct)
    {
        var result = await _svc.UpdateAsync(id, request, _currentUser.UserId!, ct);
        return Ok(result);
    }

    /// <summary>Menü öğesini sil</summary>
    [HttpDelete("api/menu-items/{id:guid}")]
    [Authorize(Roles = AppRoles.RestaurantOwner)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        await _svc.DeleteAsync(id, _currentUser.UserId!, ct);
        return NoContent();
    }

    /// <summary>Menü öğesi müsaitliğini aç/kapat</summary>
    [HttpPatch("api/menu-items/{id:guid}/availability")]
    [Authorize(Roles = $"{AppRoles.RestaurantOwner},{AppRoles.Cashier}")]
    public async Task<IActionResult> SetAvailability(
        Guid id,
        [FromBody] SetAvailabilityRequest request,
        CancellationToken ct)
    {
        // Cashier rolü için sahiplik kontrolü atlanır
        var skipOwnerCheck = _currentUser.IsInRole(AppRoles.Cashier) && !_currentUser.IsInRole(AppRoles.RestaurantOwner);
        await _svc.SetAvailabilityAsync(id, request.IsAvailable, _currentUser.UserId!, skipOwnerCheck, ct);
        return NoContent();
    }
}
