using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.Restaurants.DTOs;
using Arrivo.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arrivo.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantService _svc;
    private readonly ICurrentUserService _currentUser;

    public RestaurantController(IRestaurantService svc, ICurrentUserService currentUser)
    {
        _svc = svc;
        _currentUser = currentUser;
    }

    /// <summary>Tüm aktif restoranları listele (public)</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await _svc.GetActiveRestaurantsAsync(ct);
        return Ok(result);
    }

    /// <summary>Restoran detayı (public)</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _svc.GetByIdAsync(id, ct);
        return Ok(result);
    }

    /// <summary>Yeni restoran oluştur</summary>
    [HttpPost]
    [Authorize(Roles = $"{AppRoles.RestaurantOwner},{AppRoles.PlatformAdmin}")]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantRequest request, CancellationToken ct)
    {
        var result = await _svc.CreateAsync(request, _currentUser.UserId!, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Restoran bilgilerini güncelle (sahip kendi restoranını, admin hepsini)</summary>
    [HttpPut("{id:guid}")]
    [Authorize(Roles = $"{AppRoles.RestaurantOwner},{AppRoles.PlatformAdmin}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRestaurantRequest request, CancellationToken ct)
    {
        var isAdmin = _currentUser.IsInRole(AppRoles.PlatformAdmin);
        var result = await _svc.UpdateAsync(id, request, _currentUser.UserId!, isAdmin, ct);
        return Ok(result);
    }

    /// <summary>Restoran durumunu aktif/pasif yap (sadece platform_admin)</summary>
    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = AppRoles.PlatformAdmin)]
    public async Task<IActionResult> SetStatus(Guid id, [FromBody] SetRestaurantStatusRequest request, CancellationToken ct)
    {
        await _svc.SetStatusAsync(id, request.Activate, ct);
        return NoContent();
    }
}
