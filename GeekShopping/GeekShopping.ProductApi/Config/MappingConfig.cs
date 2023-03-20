using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;

namespace GeekShopping.ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
            config.CreateMap<ProductVO, Products>();
                config.CreateMap<Products, ProductVO>();
            });

            return mappingConfig;
        }
    }
}
