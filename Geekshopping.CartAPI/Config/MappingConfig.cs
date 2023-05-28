using AutoMapper;
using Geekshopping.CartAPI.Data.ValueObjects;
using Geekshopping.CartAPI.Model;

namespace Geekshopping.CartAPI.Config
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
