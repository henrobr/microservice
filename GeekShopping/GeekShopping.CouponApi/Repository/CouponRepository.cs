using AutoMapper;
using GeekShopping.CouponApi.Data.ValueObjects;
using GeekShopping.CouponApi.Model;
using GeekShopping.CouponApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly ServerContext context;
        private readonly IMapper mapper;

        public CouponRepository(ServerContext context, IMapper mapper)
        {                        
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);

            return mapper.Map<CouponVO>(coupon);
        }
    }
}
