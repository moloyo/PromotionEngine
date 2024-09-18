using PromotionEngine.Application.Shared;
using PromotionEngine.Entities;
using System.Runtime.CompilerServices;

namespace PromotionEngine.Application.Features.Promotions.Shared.Repositories;
public class Repository : IRepository
{
    private readonly string _connectionString;

    public Repository(IConfiguration configuration)
    {
        _connectionString = configuration["Database:ConnectionString"]!;
    }

    /// <summary>
    /// Gets all promotions as an asynchronous enumerable.
    /// </summary>
    /// <param name="countryCode">The country code.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// An <see cref="IAsyncEnumerable{T}"/> that contains the <see cref="Promotion"/> objects matching the specified country code.
    /// </returns>
    public async IAsyncEnumerable<Promotion> GetAll(string countryCode, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var databaseConnectionInfo =  new DatabaseConnection(_connectionString);
        await databaseConnectionInfo.ConnectAsync(cancellationToken);

        var query = databaseConnectionInfo
            .QueryAsync(promotion => promotion.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

        await foreach (var promotion in query.WithCancellation(cancellationToken)) 
            yield return promotion;
    }

    /// <summary>
    /// Retrieves a promotion based on the specified country code and promotion ID.
    /// </summary>
    /// <param name="countryCode">The country code associated with the promotion.</param>
    /// <param name="promotionId">The unique identifier of the promotion.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the <see cref="Promotion"/> object if found;
    /// otherwise, <c>null</c>.
    /// </returns>
    public async Task<Promotion?> GetAsync(string countryCode, Guid promotionId, CancellationToken cancellationToken)
    {
        using var databaseConnectionInfo = new DatabaseConnection(_connectionString);
        await databaseConnectionInfo.ConnectAsync(cancellationToken);

        return await databaseConnectionInfo
            .QueryAsync(promotion => promotion.CountryCode.Equals(countryCode, StringComparison.InvariantCultureIgnoreCase) && promotion.Id == promotionId, cancellationToken)
            .SingleOrDefaultAsync(cancellationToken);
    }
}
