using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.Shared.Repositories
{
    public interface IRepository
    {
        IAsyncEnumerable<Promotion> GetAll(string countryCode, CancellationToken cancellationToken);
        Task<Promotion?> GetAsync(string countryCode, Guid promotionId, CancellationToken cancellationToken);
    }
}