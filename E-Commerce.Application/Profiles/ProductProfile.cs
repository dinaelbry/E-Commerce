using AutoMapper;
using E_Commerce.Application.DTO_s.Product;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductsBrand, BrandDto>();
            CreateMap<ProductsType,TypeDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dist => dist.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dist => dist.PictureUrl, opt => opt.MapFrom <PictureUrlResolver>());

        }
         

    }
}
