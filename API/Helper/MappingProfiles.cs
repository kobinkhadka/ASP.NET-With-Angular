using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOS;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {

            //Map from product to  productoreturndto
            CreateMap<Product, ProductToReturnDTO>()
            .ForMember(destination => destination.ProductBrand, o => o.MapFrom(source => source.ProductBrand.Name))
            .ForMember(d => d.PictureUrl,  o => o.MapFrom<ProductUrlResolver>())
            .ForMember(destination => destination.ProductType, o => o.MapFrom(source => source.ProductType.Name));
        }
        
    }
}