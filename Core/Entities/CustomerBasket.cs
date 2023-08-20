using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; } 
        public decimal ShippingPrice { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public int DeliveryMethodId { get; set; }
        public CustomerBasket()
        {
                
        }

        public CustomerBasket(string id)
        {
            this.Id = id;
        }

        public List<BasketItem> Items { get; set; }
    }
}
