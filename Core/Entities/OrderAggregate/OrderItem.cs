using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
    public class OrderItem:BaseEntity
    {
        public int Quantity { get; set; }
        public ProductItemOrdered ProductItemOrdered { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }




    }
}
