using GeekShopping.CartApi.Model.Base;

namespace GeekShopping.CartApi.Model
{
    public class CartHeader : BaseEntity
    {
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
