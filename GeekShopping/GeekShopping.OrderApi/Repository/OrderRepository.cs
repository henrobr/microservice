using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<ServerContext> context;

        public OrderRepository(DbContextOptions<ServerContext> context)
        {                        
            this.context = context;
        }

        public async Task<bool> AddOrder(OrderHeader orderHeader)
        {
            if (orderHeader == null) return false;
            await using var db = new ServerContext(context);
            db.Headers.Add(orderHeader);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderId, bool status)
        {
            await using var db = new ServerContext(context);
            var header = await db.Headers.FirstOrDefaultAsync(f => f.Id == orderHeaderId);
            if(header != null)
            {
                header.PaymentStatus = status;
                await db.SaveChangesAsync();
            }
        }

    }
}
