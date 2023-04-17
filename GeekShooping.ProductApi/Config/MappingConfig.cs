using AutoMapper;
using GeekShooping.ProductApi.Data.ValueObjects;
using GeekShooping.ProductApi.Model;

namespace GeekShooping.ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProdutVO, Product>();
                config.CreateMap<CategoryVO, Category>();

                config.CreateMap<Product, ProdutVO>();
                config.CreateMap<Category, CategoryVO>();
            });
            return mappingConfig;
        }
    }
}
