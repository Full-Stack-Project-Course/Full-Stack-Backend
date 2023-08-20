using API.Dtos;
using API.Dtos.Order;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
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
            CreateMap<ShippingAddress, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomBasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<Order, OrderResponseDto>()
                .ForMember(o => o.DeliveryMethod, o => o.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(o => o.ShippingPrice, o => o.MapFrom(src => src.DeliveryMethod.Price))
                .ForMember(o => o.Total, o => o.MapFrom(src => src.getTotal()));



            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(oi => oi.ProductName, oi => oi.MapFrom(src => src.ProductItemOrdered.ProductName))
                .ForMember(oi => oi.ProductItemID, oi => oi.MapFrom(src => src.ProductItemOrdered.ProductItemID))
                .ForMember(oi => oi.PictureURL, oi => oi.MapFrom<OrderItemURLResolver>());

         
        }
    }
}
