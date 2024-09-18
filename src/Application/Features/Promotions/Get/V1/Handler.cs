using Mapster;
using PromotionEngine.Application.Features.Promotions.Shared;
using PromotionEngine.Application.Features.Promotions.Shared.Repositories;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.Features.Promotions.Get.V1;

public class Handler : IHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the request to retrieve a promotion by country code and promotion ID.
    /// </summary>
    /// <param name="request">The request containing the country code, promotion ID, and language code.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task for the <see cref="Response"/> object which includes the promotion data if found, 
    /// a not found status if the promotion doesn't exist, or an exception if an error occurs during processing.
    /// </returns>
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        var response = new Response();

        try
        {
            var promotion = await _repository.GetAsync(request.CountryCode, request.PromotionId, cancellationToken);

            if (promotion is null)
                return response.SetNotFound();

            var promotionModel = promotion.Adapt<PromotionModel>();

            if (promotion.DisplayContent != null && promotion.DisplayContent.TryGetValue(request.LanguageCode, out var displayContent))
            {
                promotionModel.Texts = displayContent.Adapt<PromotionTextsModel>();
            }

            return response.SetPromotion(promotionModel);
        }
        catch (Exception ex)
        {
            return response
                .SetException(ex);
        }
    }
}
