using AutoMapper;
using onine.core.Entites;
using onineproject.DTOs;

namespace onineproject.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() { 
            CreateMap<Product,ProductToReturnDTOs>()
                .ForMember(d=> d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d=> d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductpictureurlResolver>());
            



        }

        
    }
}
