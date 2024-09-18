using Mapster;
using PromotionEngine.Application.Features.Promotions.Shared;
using PromotionEngine.Application.Features.Promotions.Shared.Repositories;
using PromotionEngine.Application.Shared;

namespace PromotionEngine.Application.Features.Promotions.GetAll.V1;

public class Handler : IHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response> HandleAsync(Request request, CancellationToken cancellationToken = default)
    {
        var response = new Response();

        try
        {          
            var promotions = _repository.GetAll(request.CountryCode, cancellationToken);

            var promotionModels = new List<PromotionModel>();

            await foreach (var promotion in promotions)
            {
                var promotionModel = promotion.Adapt<PromotionModel>();

                if (promotion.DisplayContent != null && promotion.DisplayContent.TryGetValue(request.LanguageCode, out var displayContent))
                {
                    promotionModel.Texts = displayContent.Adapt<PromotionTextsModel>();
                }

                promotionModels.Add(promotionModel);
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
