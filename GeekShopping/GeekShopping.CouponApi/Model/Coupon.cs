using GeekShopping.CouponApi.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponApi.Model
{
    public class Coupon : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string CouponCode { get; set; }
        [Required]
        public decimal DiscountAmount { get; set; }

    }
}
