using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class OrderItemURLResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;
        public OrderItemURLResolver(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureURL))
            {
                string baseUri = _configuration["ApiUri"];
                return  source.ProductItemOrdered.PictureURL;
            }

            return null;
        }
    }

}
