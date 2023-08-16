using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class OrderWithMethodanditemsSpec:BaseSpecification<Order>
    {
        public OrderWithMethodanditemsSpec(string email):base(o=> o.BuyerEmail == email)
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.Items);
            SetOrderByDescending(o => o.OrderDate);
        }

        public OrderWithMethodanditemsSpec(int id , string email):base(o => o.Id == id && o.BuyerEmail == email) 
        {
            AddIncludes(o => o.DeliveryMethod);
            AddIncludes(o => o.Items);
            SetOrderByDescending(o => o.OrderDate);
        }
    }
}
