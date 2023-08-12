using Core.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductsWithFilterForCountSpec:BaseSpecification<Product> 
    {
        public ProductsWithFilterForCountSpec(ProductSpecificationParams proDto) : base(pro => (string.IsNullOrEmpty(proDto.search) || pro.Name.ToLower().Contains(proDto.search.ToLower())) &&
                    (!proDto.brandID.HasValue || pro.ProductBrandId == proDto.brandID) &&
                    (!proDto.typeID.HasValue || pro.ProductTypeId == proDto.typeID)

            )
        {
            
        }
    }
}
