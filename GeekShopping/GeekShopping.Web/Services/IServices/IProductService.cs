using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAllProducts(string token);
        Task<ProductViewModel> FindByIdProducts(long id, string token);
        Task<ProductViewModel> CreateProducts(ProductViewModel model, string token);
        Task<ProductViewModel> UpdateProducts(ProductViewModel model, string token);
        Task<bool> DeleteByIdProducts(long id, string token);
    }
}
