using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;
using GeekShopping.ProductApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ServerContext context;
        private readonly IMapper mapper;

        public ProductRepository(ServerContext context, IMapper mapper)
        {                        
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Products> products = await context.Products.ToListAsync();
            return mapper.Map<List<ProductVO>>(products);
        }

        public async Task<ProductVO> FindById(long id)
        {
            Products product = await context.Products.Where(p => p.Id == id).SingleOrDefaultAsync() ?? new Products();
            return mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Create(ProductVO vo)
        {
            Products product = mapper.Map<Products>(vo);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return mapper.Map<ProductVO>(product);
        }

        public async Task<ProductVO> Update(ProductVO vo)
        {
            Products product = mapper.Map<Products>(vo);
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return mapper.Map<ProductVO>(product);
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                Products product = await context.Products.Where(p => p.Id == id).SingleOrDefaultAsync() ?? new Products();
                if (product.Id < 1) return false;

                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
