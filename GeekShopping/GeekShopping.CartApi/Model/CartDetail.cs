using GeekShopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{
    public class CartDetail : BaseEntity
    {
        public long CartHeaderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }

        //CartHeader
        [ForeignKey(nameof(CartHeaderId))]
        public CartHeader CartHeaderIdNavigation { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Products ProductIdNavigation { get; set; }

    }
}
