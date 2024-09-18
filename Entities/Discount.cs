using PromotionEngine.Entities.Converters;
using System.Text.Json.Serialization;

namespace PromotionEngine.Entities;


[JsonConverter(typeof(PromotionDiscountJsonConverter))]
public abstract class Discount
{
    public abstract DiscountType Type { get; }

    public decimal? OriginalPrice { get; set; }

    public decimal? FinalPrice { get; set; }

    public decimal? LowestPriceLast30Days { get; set; }

    public string? PriceType { get; set; }

    public int UnitsToBuy { get; set; }

    public int UnitsToPay { get; set; }

    public bool HasPrice { get; set; }
}
