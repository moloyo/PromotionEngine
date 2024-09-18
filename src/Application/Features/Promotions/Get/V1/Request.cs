using PromotionEngine.Entities;

namespace PromotionEngine.Application.Features.Promotions.Get.V1;

public record struct Request(string CountryCode, string LanguageCode, Guid PromotionId);
