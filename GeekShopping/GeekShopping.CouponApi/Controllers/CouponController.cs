using GeekShopping.CouponApi.Data.ValueObjects;
using GeekShopping.CouponApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CouponApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private ICouponRepository repository;

        public CouponController(ICouponRepository repository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{couponCode}")]
        //[Authorize]
        public async Task<ActionResult<CouponVO>> GetCoupon(string couponCode)
        {
            var coupon = await repository.GetCouponByCouponCode(couponCode);

            if (coupon == null) return NotFound();

            return Ok(coupon);
        }
    }
}
