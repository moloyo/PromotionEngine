using Mapster;
using PromotionEngine.Application.Features.Promotions.Shared;
using PromotionEngine.Application.Features.Promotions.Shared.Repositories;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

public class Handler : IHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the request to retrieve a max number of promotions for a specified country code. Is no max is selected it will return every promotion for that country code.
    /// </summary>
    /// <param name="request">The request containing the country code, language code, and optional maximum number of promotions to return.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>
    /// A task for the <see cref="Response"/> object with the list of promotions for the specified country, or an exception if an error occurs during processing.
    /// </returns>
    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        var response = new Response();

        try
        {
            var promotions = _repository.GetAll(request.CountryCode, cancellationToken);

            var promotionModels = new List<PromotionModel>();

            var count = 0;

            await foreach (var promotion in promotions)
            {
                if (!request.MaxPromotions.HasValue || count < request.MaxPromotions)
                {
                    var promotionModel = promotion.Adapt<PromotionModel>();

                    if (promotion.DisplayContent != null && promotion.DisplayContent.TryGetValue(request.LanguageCode, out var displayContent))
                    {
                        promotionModel.Texts = displayContent.Adapt<PromotionTextsModel>();
                    }

                    promotionModels.Add(promotionModel);
                    count++;
                }
            }

            return response
                .SetPromotions(promotionModels);
        }
        catch (Exception ex)
        {
            return response
                .SetException(ex);
        }
    }
}
