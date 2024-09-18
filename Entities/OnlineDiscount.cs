
namespace PromotionEngine.Entities;
public class OnlineDiscount : Discount
{
    public override DiscountType Type => DiscountType.Online;
}
