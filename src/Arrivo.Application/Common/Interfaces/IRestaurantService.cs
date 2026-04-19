using Arrivo.Application.Features.Restaurants.DTOs;

namespace Arrivo.Application.Common.Interfaces;

public interface IRestaurantService
{
    Task<IEnumerable<RestaurantListDto>> GetActiveRestaurantsAsync(CancellationToken ct = default);
    Task<RestaurantDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<RestaurantDto> CreateAsync(CreateRestaurantRequest request, string ownerId, CancellationToken ct = default);
    Task<RestaurantDto> UpdateAsync(Guid id, UpdateRestaurantRequest request, string requestingUserId, CancellationToken ct = default);
    Task ApproveAsync(Guid id, CancellationToken ct = default);
    Task RejectAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<RestaurantDto>> GetPendingAsync(CancellationToken ct = default);
}
