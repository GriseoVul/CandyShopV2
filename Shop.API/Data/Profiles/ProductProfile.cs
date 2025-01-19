using System;
using AutoMapper;

namespace Shop.API.Data.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Entities.Product.FoodValue, Models.Product.FoodValueDTO>();
            CreateMap<Entities.Product.ProductCharacteristic, Models.Product.ProductCharacteristicDTO>();
            CreateMap<Entities.Product.ProductDescription, Models.Product.ProductDescriptionDTO>();

            CreateMap<Entities.Product.Product, Models.Product.ProductDto>();
            CreateMap<Entities.Product.Product, Models.Product.ProductWithoutDescriptionDTO>();
        }
    }
}
