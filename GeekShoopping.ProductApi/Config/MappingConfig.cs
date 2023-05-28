using AutoMapper;
using GeekShoopping.ProductApi.Data.ValueObjects;
using GeekShoopping.ProductApi.Model;

namespace GeekShoopping.ProductApi.Config
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
