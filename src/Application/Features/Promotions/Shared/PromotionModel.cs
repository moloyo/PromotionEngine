namespace PromotionEngine.Application.Features.Promotions.Shared;

public class PromotionModel
{
    public Guid PromotionId { get; set; }
    public PromotionTextsModel? Texts { get; set; }
    public IEnumerable<string> Images { get; set; } = [];
    public IEnumerable<PromotionDiscountModel>? Discounts { get; set; }
    public DateTime EndValidityDate { get; set; }
}

public class PromotionTextsModel
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? DiscountTitle { get; set; }
    public string? DiscountDescription { get; set; }
}

public class PromotionDiscountModel
{
    public required string Type { get; set; }
    public decimal? OriginalPrice { get; set; }
    public decimal? FinalPrice { get; set; }
    public decimal? LowestPriceLast30Days { get; set; }
    public string? PriceType { get; set; }
    public int UnitsToBuy { get; set; }
    public int UnitsToPay { get; set; }
    public bool HasPrice { get; set; }
}

