using Core.Entities.OrderAggregate;

namespace API.Dtos.Order
{
    public class OrderResponseDto
    {
        public IReadOnlyList<OrderItemDto> Items { get; set; }

        public string Status { get; set; }
        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal ShippingPrice { get; set; }
        public AddressDto Address { get; set; }

        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string PaymentIntentId { get; set; }
    }
}
