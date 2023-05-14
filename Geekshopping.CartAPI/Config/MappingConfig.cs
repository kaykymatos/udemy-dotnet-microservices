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
                config.CreateMap<CartVO, CartHeader>();
                config.CreateMap<CartHeader, CartVO>();
            });
            return mappingConfig;
        }
    }
}
