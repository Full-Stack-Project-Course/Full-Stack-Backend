using Core.Entities.OrderAggregate;


namespace API.Dtos
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public int ProductItemID { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }

        public decimal Price { get; set; }
    }
}
