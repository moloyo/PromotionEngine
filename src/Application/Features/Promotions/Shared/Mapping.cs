using Mapster;
using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.Shared
{
    public class Mapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Promotion, PromotionModel>()
                .Map(dest => dest.PromotionId, src => src.Id);
        }
    }
}
