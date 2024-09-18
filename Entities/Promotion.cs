namespace PromotionEngine.Entities;

public class Promotion
{
    public Guid Id { get; set; }
    public required PromotionStatus Status { get; set; }
    public List<Discount>? Discounts { get; set; }
    public Dictionary<string, DisplayContent>? DisplayContent { get; set; }
    public required string CountryCode { get; set; }
    public required DateTime CreatedDate { get; set; }
    public required DateTime LastModifiedDate { get; set; }
    public DateTime EndValidityDate { get; set; }
    public required List<string> Images { get; set; }
}
