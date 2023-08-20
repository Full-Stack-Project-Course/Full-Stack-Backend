using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        public PaymentService(IBasketRepository basketRepository , IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
            StripeConfiguration.ApiKey = configuration["StripeConfiguration:SecretKey"];
            StripeConfiguration.ClientId = configuration["StripeConfiguration:PublishKey"];

        }
        public async Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketID)
        {
            var basket = await _basketRepository.GetBasketAsync(basketID);



            if (basket is null) { return null; }

            //make sure the prices from basket is the same as the database
            foreach(var item in basket.Items)
            {
              var productItem =  await _unitOfWork.Repository<Core.Entities.Product>().GetOneByIDAsync(item.Id);
              item.Price = item.Price == (decimal)productItem!.Price ? (decimal)productItem.Price : item.Price;
            }

            var service = new PaymentIntentService();
            if(basket.PaymentIntentId == "")
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long?)basket.Items.Sum(item => (item.Quantity * (item.Price * 100)) +  (basket.ShippingPrice * 100)),
                    Currency = "EGP",
                    PaymentMethodTypes = new List<string> { "card" }


                };
                var intent = await service.CreateAsync(options);

                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long?)basket.Items.Sum(item => (item.Quantity * (item.Price * 100)) + (basket.ShippingPrice * 100)),
                    Currency = "EGP",

                };

                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order?> UpdateOrderStatusWithFailure(string paymentIntentID)
        {
            var spec = new OrderWithPaymentIntentSpec(paymentIntentID);

            var order = await _unitOfWork.Repository<Order>().GetOneEntityWithSpec(spec);

            if(order is null) { return null; }

            order.Status = OrderStatus.PaymentFailed;

            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Compelete();
            return order;
        }

        public async Task<Order?> UpdateOrderStatusWithSuccess(string paymentIntentID)
        {
            var spec = new OrderWithPaymentIntentSpec(paymentIntentID);

            var order = await _unitOfWork.Repository<Order>().GetOneEntityWithSpec(spec);

            if (order is null) { return null; }

            order.Status = OrderStatus.PaymentReceived;
            _unitOfWork.Repository<Order>().Update(order);
            await _unitOfWork.Compelete();

            return order;
        }
    }
}
