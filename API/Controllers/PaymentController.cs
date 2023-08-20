using API.Dtos;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        private const string WhSecret = "whsec_5fc23255c0438454f38787fd8fbb8e90043d6f03e6270ce65fdab1aa8b1d3395";
        public PaymentController(IPaymentService paymentService , ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }
        [HttpPost("{basketId}")]
        [Authorize]
 
        public async Task<ActionResult<CustomBasketDto>> CreateOrUpdatePayment(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntent(basketId);

            if(basket is null) { return BadRequest( new ApiResponse(403 , "please send a valid basket ID") ); }

            return Ok(basket);
        }

        [HttpPost("webhook")]

        public async Task<ActionResult> UpdateOrder()
        {
            var reader = await new StreamReader(Request.Body).ReadToEndAsync();

            
            var stripeEvent = EventUtility.ConstructEvent(reader, Request.Headers["Stripe-Signature"],WhSecret,throwOnApiVersionMismatch:false);

            PaymentIntent intent;
            
              switch(stripeEvent.Type) {
                case "payment_intent.succeeded": {
                        intent = stripeEvent.Data.Object as PaymentIntent;
                        await _paymentService.UpdateOrderStatusWithSuccess(intent.Id);
                        _logger.LogInformation("Order Updated Successfully");

                        break;
                }

                 case "payment_intent.payment_failed": {
                        intent = stripeEvent.Data.Object as PaymentIntent;
                        await _paymentService.UpdateOrderStatusWithFailure(intent.Id);
                        _logger.LogError("Payment Failed");
                        break;
                 }
            }


            return Ok();

        }
    }
}
