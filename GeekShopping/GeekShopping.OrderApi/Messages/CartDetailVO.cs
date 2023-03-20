namespace GeekShopping.OrderApi.Messages
{
    public class CartDetailVO
    {
        public long Id { get; set; }
        public long CartHeaderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public ProductsVO ProductIdNavigation { get; set; }

    }
}
