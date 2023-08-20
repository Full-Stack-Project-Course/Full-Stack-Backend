namespace API.Dtos.Order
{
    public class OrderRequestDto
    {
        public int DeliveryMethodId { get; set; }
        public string BasketID { get; set; }
        public string PaymentIntentId { get; set; }

        public AddressDto ShipToAddress { get; set; }
    }
}
