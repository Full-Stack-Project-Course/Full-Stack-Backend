using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Identity;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
       
        public MappingProfiles()
        {
            CreateMap<Product, ProdcutToReturnDto>()
                .ForMember(dto => dto.ProductBrand, o => o.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dto => dto.ProductType, o => o.MapFrom(src => src.ProductType.Name))
                .ForMember(dto => dto.PictureUrl , o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Address , AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
