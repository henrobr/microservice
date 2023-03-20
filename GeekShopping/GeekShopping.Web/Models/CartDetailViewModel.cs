namespace GeekShopping.Web.Models
{
    public class CartDetailViewModel
    {
        public long Id { get; set; }
        public long CartHeaderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }

        public CartHeaderViewModel CartHeaderIdNavigation { get; set; }
        public ProductViewModel ProductIdNavigation { get; set; }

    }
}
