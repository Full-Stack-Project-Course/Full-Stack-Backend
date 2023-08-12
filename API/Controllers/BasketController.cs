using Core.Entities;
using API.Errors;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepo = basketRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> Basket(string id) {
            return await _basketRepo.GetBasketAsync(id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerBasket>> DeleteBasket(string id)
        {
            var deleted = await _basketRepo.DeleteBasketAsync(id);
            if(!deleted) { return BadRequest(new ApiResponse(400, "Invalid ID Was Send")); }
            return Ok(new ApiResponse(200 , "Record Deleted Successfully"));
        }

        [HttpPatch]
        
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomBasketDto customerBasket)
        {
            var UpdatedBasket = await _basketRepo.UpdateBasketAsync(_mapper.Map<CustomerBasket>(customerBasket));

            return UpdatedBasket ?? new CustomerBasket(customerBasket.Id);
        }
    }
}
