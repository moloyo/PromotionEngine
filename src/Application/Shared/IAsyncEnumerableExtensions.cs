namespace PromotionEngine.Application.Shared;

public static class IAsyncEnumerableExtensions
{
    public static async Task<IEnumerable<T>> ToListAsync<T>(this IAsyncEnumerable<T> asyncEnumerable, CancellationToken cancellationToken)
    {
        var unrolledAsyncEnumerable = new List<T>();
        await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
        {
            unrolledAsyncEnumerable.Add(item);
        }

        return unrolledAsyncEnumerable;
    }

    public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> asyncEnumerable, CancellationToken cancellationToken)
    {
        T? result = default;
        var hasItem = false;

        await foreach (var item in asyncEnumerable.WithCancellation(cancellationToken))
        {
            if (hasItem)
                throw new InvalidOperationException("Sequence contains more than one element.");

            result = item;
            hasItem = true;
        }

        return result;
    }
}
