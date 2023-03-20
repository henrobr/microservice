using AutoMapper;
using GeekShopping.CartApi.Data.ValueObjects;
using GeekShopping.CartApi.Model;
using GeekShopping.CartApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ServerContext context;
        private readonly IMapper mapper;

        public CartRepository(ServerContext context, IMapper mapper)
        {                        
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var header = await context.CartHeaders.FirstOrDefaultAsync(f => f.UserId == userId);
            if (header != null)
            {
                header.CouponCode = couponCode;
                context.CartHeaders.Update(header);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveCoupon(string userId)
        {
            var header = await context.CartHeaders.FirstOrDefaultAsync(f => f.UserId == userId);
            if (header != null)
            {
                header.CouponCode = "";
                context.CartHeaders.Update(header);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeader = await context.CartHeaders.FirstOrDefaultAsync(f => f.UserId == userId);
            if(cartHeader != null)
            {
                context.CartDetails.RemoveRange(context.CartDetails.Where(w => w.CartHeaderId == cartHeader.Id));
                context.CartHeaders.Remove(cartHeader);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CartVO> FindCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await context.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId),
            };
            cart.CartDetails = context.CartDetails.Where(w => w.CartHeaderId == cart.CartHeader.Id).Include(i => i.ProductIdNavigation);

            return mapper.Map<CartVO>(cart);
        }


        public async Task<bool> RemoveFromCart(long id)
        {
            try
            {
                CartDetail cartDetail = await context.CartDetails.FirstOrDefaultAsync(c => c.Id == id);

                int total = context.CartDetails.Where(w => w.CartHeaderId == cartDetail.CartHeaderId).Count();

                context.CartDetails.Remove(cartDetail);

                if(total == 1)
                {
                    var cartHeaderToRemove = await context.CartHeaders.FirstOrDefaultAsync(f => f.Id == cartDetail.CartHeaderId);
                    context.CartHeaders.Remove(cartHeaderToRemove);
                }
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO vo)
        {
            Cart cart = mapper.Map<Cart>(vo);
            //Checks if product is already saved in the database if it does not exist then save
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == vo.CartDetails.FirstOrDefault().ProductId);
            
            if(product == null)
            {
                context.Products.Add(cart.CartDetails.FirstOrDefault().ProductIdNavigation);
                await context.SaveChangesAsync();
            }

            //Check if CartHeader is null
            var cartHeader = await context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);
            
            if(cartHeader == null)
            {
                context.CartHeaders.Add(cart.CartHeader);
                await context.SaveChangesAsync();

                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().ProductIdNavigation = null;
                context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await context.SaveChangesAsync();
            }
            else
            {
                //If CartHeader is not null
                //Check if CartDetails has same product
                var cartDetail = await context.CartDetails.AsNoTracking().FirstOrDefaultAsync(d => d.ProductId == cart.CartDetails.FirstOrDefault().ProductId && d.CartHeaderId == cartHeader.Id);
                if(cartDetail == null)
                {
                    //Create CartDetails
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().ProductIdNavigation = null;
                    context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await context.SaveChangesAsync();

                }
                else
                {
                    //Update product count and CartDetails
                    cart.CartDetails.FirstOrDefault().ProductIdNavigation = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await context.SaveChangesAsync();
                }
            }

            return mapper.Map<CartVO>(cart);
        }
    }
}
