using PromotionEngine.Application.Features.Promotions.Shared;

namespace PromotionEngine.Application.Features.Promotions.Get.V1;

public class Response
{
    public PromotionModel? Promotion { get; private set; }

    [JsonIgnore]
    public bool NotFound { get; private set; }

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

    public Response SetPromotion(PromotionModel promotion)
    {
        Promotion = promotion;

        return this;
    }
    public Response SetNotFound()
    {
        NotFound = true;

        return this;
    }
}
