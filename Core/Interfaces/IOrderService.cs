using Core.Entities;
using Core.Entities.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string BuyerEmail, int DeliveryMethodId, string BasktetID, ShippingAddress ShippingAddress, string PaymentIntentId);

        public Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();

        public Task<IReadOnlyList<Order>> GetOrdersByUserEmailAsync(string Email);

        public Task<Order> GetOrderByIDAsync(int ID, string email);
    }
}
