using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrderWithPaymentIntentSpec:BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpec(string paymentIntentID) :base(order => order.PaymentIntentId == paymentIntentID)
        { }
    }
}
