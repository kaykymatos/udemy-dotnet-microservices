using AutoMapper;
using Geekshopping.CartApi.Data.ValueObjects;
using Geekshopping.CartApi.Model;

namespace Geekshopping.CartApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            MapperConfiguration mappingConfig = new(config =>
            {
                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                config.CreateMap<CartVO, Cart>().ReverseMap();
                config.CreateMap<Product, ProductVO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
