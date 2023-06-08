using AutoMapper;
using GeekShopping.CuponApi.Data.ValueObjects;
using GeekShopping.CuponApi.Model;

namespace GeekShopping.CuponApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new(config =>
            {
                config.CreateMap<Cupon, CuponVO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
