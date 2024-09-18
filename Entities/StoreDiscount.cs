namespace PromotionEngine.Entities;
public class StoreDiscount : Discount
{
    public override DiscountType Type => DiscountType.Store;
}
