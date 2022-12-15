using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(s => s.Brand.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(s => s.Category.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductUrlResolver>());
        }
    }
}