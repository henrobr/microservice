using GeekShopping.CartApi.Model;

namespace GeekShopping.CartApi.Data.ValueObjects
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetail> CartDetails { get; set; }
    }
}
