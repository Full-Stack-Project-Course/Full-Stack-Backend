using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderRepository(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;

        }

        public async Task<Order?> CreateOrderAsync(string BuyerEmail,int DeliveryMethodId, string BasktetID, Address ShippingAddress)
        {
            //get the basket
            var basket = await _basketRepository.GetBasketAsync(BasktetID);
            var items = basket.Items;
            var orderItems = new List<OrderItem>();
            
            foreach(var product in items)
            {
                var productOrdered = new ProductItemOrdered { PictureURL = product.PictureURL, ProductItemID = product.Id, ProductName = product.ProductName };
                var OrderItem = new OrderItem { Quantity = product.Quantity,ProductItemOrdered = productOrdered , Price=product.Price  };
                orderItems.Add(OrderItem);
            }
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetOneByIDAsync(DeliveryMethodId);
            var totalSum = orderItems.Sum(o => o.Price * o.Quantity);
            var order = new Order
            {
                Address = ShippingAddress,
                BuyerEmail = BuyerEmail,
                DeliveryMethod = deliveryMethod,
                Items = orderItems,
                SubTotal = totalSum,
            };

           await _unitOfWork.Repository<Order>().Add(order);
           var result= await _unitOfWork.Compelete();

            //delete the basket
          // var res = await _basketRepository.DeleteBasketAsync(BasktetID);
            if (result<=0)
            {
                return null;
            }
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync(); 
        }

        public async Task<Order?> GetOrderByIDAsync(int ID , string email)
        {
            var spec = new OrderWithMethodanditemsSpec(ID , email);

            return await _unitOfWork.Repository<Order>().GetOneEntityWithSpec(spec);
        }

        public Task<IReadOnlyList<Order>> GetOrdersByUserEmailAsync(string Email)
        {
            var spec = new OrderWithMethodanditemsSpec(Email);

            return _unitOfWork.Repository<Order>().ListEntityWithSpec(spec);
        }
    }
}
