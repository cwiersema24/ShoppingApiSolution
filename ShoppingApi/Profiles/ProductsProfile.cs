using AutoMapper;
using ShoppingApi.Data;
using ShoppingApi.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Profiles
{
    public class ProductsProfile:Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, GetProductDetailResponse>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));
        }
    }
}
