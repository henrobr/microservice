using GeekShopping.OrderApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderApi.Model
{
    public class OrderDetail : BaseEntity
    {
        public long OrderHeaderId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }


        //OrderHeader
        [ForeignKey(nameof(OrderHeaderId))]
        public OrderHeader OrderHeader { get; set; }

    }
}
