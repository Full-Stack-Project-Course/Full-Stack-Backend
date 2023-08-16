using Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Order:BaseEntity
    {
        public IReadOnlyList<OrderItem> Items { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal SubTotal { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public Address Address { get; set; }

        public string BuyerEmail { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string PaymentIntentId { get; set; }

        public decimal getTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }





    }
}
