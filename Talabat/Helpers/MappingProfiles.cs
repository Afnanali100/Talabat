using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Dtos;

namespace Talabat.Helpers
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDtos>()
                .ForMember(Do => Do.ProductBrand, O => O.MapFrom(P => P.ProductBrand.Name))
                .ForMember(Do => Do.ProductType, O => O.MapFrom(P => P.ProductType.Name))
                .ForMember(Do => Do.PictureUrl, O => O.MapFrom<PictureUrlResolver>());
                ;
        }

    }

}
