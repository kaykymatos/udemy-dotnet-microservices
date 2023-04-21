﻿using AutoMapper;
using GeekShoopping.ProductApi.Data.ValueObjects;
using GeekShoopping.ProductApi.Model;

namespace GeekShoopping.ProductApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>();

                config.CreateMap<Product, ProductVO>();
            });
            return mappingConfig;
        }
    }
}
