namespace PromotionEngine.Application.Features.Promotions.GetAll.V2;

public record struct Request(string CountryCode, string LanguageCode, int? MaxPromotions);
