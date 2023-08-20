using Core.Entities;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketID);

        Task<Order?> UpdateOrderStatusWithSuccess(string paymentIntentID);


        Task<Order?> UpdateOrderStatusWithFailure(string paymentIntentID);

    }
}
