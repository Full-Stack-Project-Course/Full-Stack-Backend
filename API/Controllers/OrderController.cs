using API.Dtos;
using API.Dtos.Order;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService , IMapper mapper)
        {
             _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> Order(OrderRequestDto orderDto)
        {
            var email = HttpContext.User.GetUserEmail();
            var address = _mapper.Map<AddressDto, ShippingAddress>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketID, address , orderDto.PaymentIntentId);
            
            if(order is null) { return BadRequest(new ApiResponse(401, "Please Send Valid Data") ); }

  
            
            return Ok(order);

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderResponseDto>>> GetOrderByUserEmail()
        {
            var email = HttpContext.User.GetUserEmail();
            var orders = await _orderService.GetOrdersByUserEmailAsync(email);

            return Ok(_mapper.Map<IReadOnlyList<OrderResponseDto>>(orders));
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<OrderResponseDto>>> GetOrderByIDForUser(int id)
        {
            var email = HttpContext.User.GetUserEmail();
            var order = await _orderService.GetOrderByIDAsync(id , email);

            if(order is null) { return BadRequest(new ApiResponse(403));}
            return Ok(_mapper.Map<OrderResponseDto>(order));
        }

        [HttpGet("DeliveryMethods")]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetAllDeliveryMethodsAsync());
        }
    }
}
