using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.NewFolder;
using GeekShopping.CartApi.RabbitMQSender;
using GeekShopping.CartApi.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.CartApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository repository;
        private ICouponRepository couponRepository;
        private IRabbitMQSender rabbitMQMessageSender;

        public CartController(ICartRepository repository, IRabbitMQSender rabbitMQMessageSender, ICouponRepository couponRepository)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
            this.couponRepository = couponRepository;
        }

        [HttpGet("Find/{id}")]
        public async Task<ActionResult<ProductsVO>> FindById(string id)
        {
            var cart = await repository.FindCartByUserId(id);

            if (cart == null) return NotFound();

            return Ok(cart);
        }
        [HttpPost("Add")]
        public async Task<ActionResult<ProductsVO>> AddCart(CartVO vo)
        {
            var cart = await repository.SaveOrUpdateCart(vo);

            if (cart == null) return NotFound();

            return Ok(cart);
        }
        [HttpPut("Update")]
        public async Task<ActionResult<ProductsVO>> UpdateCart(CartVO vo)
        {
            var cart = await repository.SaveOrUpdateCart(vo);

            if (cart == null) return NotFound();

            return Ok(cart);
        }
        [HttpDelete("Remove/{id}")]
        public async Task<ActionResult<ProductsVO>> RemoveCart(int id)
        {
            var status = await repository.RemoveFromCart(id);

            if (!status) return BadRequest();

            return Ok(status);
        }
        [HttpPost("ApplyCoupon")]
        public async Task<ActionResult<ProductsVO>> ApplyCoupon(CartVO vo)
        {
            var status = await repository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode);

            if (!status) return NotFound();

            return Ok(status);
        }
        [HttpDelete("RemoveCoupon/{userId}")]
        public async Task<ActionResult<ProductsVO>> RemoveCoupon(string userId)
        {
            var status = await repository.RemoveCoupon(userId);

            if (!status) return NotFound();

            return Ok(status);
        }
        [HttpPost("Checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            //string token = Request.Headers["Authorization"];
            var token = await HttpContext.GetTokenAsync("access_token");
            if (vo?.UserId == null) return BadRequest();
            var cart = await repository.FindCartByUserId(vo.UserId);
            if (cart == null) return NotFound();
            if (!string.IsNullOrEmpty(vo.CouponCode))
            {
                CouponVO coupon = await couponRepository.GetCoupon(vo.CouponCode, token);
                if (vo.DiscountAmount != coupon.DiscountAmount) return StatusCode(412);
            }
            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            //TASK RabbitMQ logic comes here!!!
            rabbitMQMessageSender.SendMessage(vo, "checkoutqueue");

            return Ok(vo);
        }
    }
}