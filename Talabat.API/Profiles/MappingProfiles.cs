using AutoMapper;
using Talabat.API.DTOS;
using Talabat.Core.Entities;

namespace Talabat.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>().ForMember(m => m.ProductBrand, o => o.MapFrom(f => f.ProductBrand.Name)).ForMember(m => m.ProductType, o => o.MapFrom(f => f.ProductType.Name))
                .ForMember(m=>m.PictureUrl, o=>o.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
