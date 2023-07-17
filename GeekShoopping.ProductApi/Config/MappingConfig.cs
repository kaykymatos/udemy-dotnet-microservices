using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model;

namespace GeekShopping.ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new(config =>
            {
                config.CreateMap<ProductVO, Product>();

                config.CreateMap<Product, ProductVO>();
            });
            return mappingConfig;
        }
    }
}
