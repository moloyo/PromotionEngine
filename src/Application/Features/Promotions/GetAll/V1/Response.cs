using PromotionEngine.Application.Features.Promotions.Shared;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

public class Response
{
    public IEnumerable<PromotionModel> Promotions { get; private set; } = Enumerable.Empty<PromotionModel>();

    [JsonIgnore]
    public bool ExceptionOccurred { get; private set; }

    [JsonIgnore]
    public Exception? Exception { get; private set; }

    public Response SetException(Exception ex)
    {
        ArgumentNullException.ThrowIfNull(ex, nameof(ex));

        ExceptionOccurred = true;
        Exception = ex;

        return this;
    }

    public Response SetPromotions(IEnumerable<PromotionModel> promotions)
    {
        Promotions = promotions;

        return this;
    }
}
