using Arrivo.Application.Common.Interfaces;
using Arrivo.Application.Features.Orders.DTOs;
using Arrivo.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arrivo.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _svc;
    private readonly ICurrentUserService _currentUser;

    public OrderController(IOrderService svc, ICurrentUserService currentUser)
    {
        _svc = svc;
        _currentUser = currentUser;
    }

    /// <summary>Yeni sipariş ver</summary>
    [HttpPost]
    [Authorize(Roles = $"{AppRoles.Customer},{AppRoles.Cashier}")]
    public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest request, CancellationToken ct)
    {
        var result = await _svc.PlaceOrderAsync(request, _currentUser.UserId!, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>Giriş yapan kullanıcının siparişlerini listele</summary>
    [HttpGet]
    public async Task<IActionResult> GetMyOrders(CancellationToken ct)
    {
        var result = await _svc.GetOrdersAsync(_currentUser.UserId!, restaurantId: null, ct);
        return Ok(result);
    }

    /// <summary>Belirli bir siparişin detayı</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _svc.GetByIdAsync(id, _currentUser.UserId!, ct);
        return Ok(result);
    }

    /// <summary>Restoranın tüm siparişlerini listele</summary>
    [HttpGet("restaurant/{restaurantId:guid}")]
    [Authorize(Roles = $"{AppRoles.RestaurantOwner},{AppRoles.Cashier},{AppRoles.PlatformAdmin}")]
    public async Task<IActionResult> GetByRestaurant(Guid restaurantId, CancellationToken ct)
    {
        var result = await _svc.GetOrdersAsync(_currentUser.UserId!, restaurantId, ct);
        return Ok(result);
    }

    /// <summary>Sipariş durumunu güncelle (Pending→Confirmed→Preparing→Ready→Delivered/Cancelled)</summary>
    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = $"{AppRoles.RestaurantOwner},{AppRoles.Cashier}")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateOrderStatusRequest request, CancellationToken ct)
    {
        var result = await _svc.UpdateStatusAsync(id, request, _currentUser.UserId!, ct);
        return Ok(result);
    }
}
