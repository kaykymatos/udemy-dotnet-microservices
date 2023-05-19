using AutoMapper;
using Geekshopping.CartAPI.Data.ValueObjects;
using Geekshopping.CartAPI.Model;

namespace Geekshopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartVO, Cart>().ReverseMap();
                config.CreateMap<CartDetailVO, CartDetail>().ReverseMap();
                config.CreateMap<CartHeaderVO, CartHeader>().ReverseMap();
                config.CreateMap<Product, ProductVO>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
