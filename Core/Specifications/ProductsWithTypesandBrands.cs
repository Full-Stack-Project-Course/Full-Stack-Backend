using Core.Entities;
using Core.Specifications;
using System.Linq.Expressions;

namespace Infrastructure.Data
{
    public class ProductsWithTypesandBrands : BaseSpecification<Product> 
    {
        public ProductsWithTypesandBrands(ProductSpecificationParams proDto) 
            :base(pro =>( string.IsNullOrEmpty(proDto.search) || pro.Name.ToLower().Contains(proDto.search.ToLower())) &&
                    (!proDto.brandID.HasValue || pro.ProductBrandId == proDto.brandID) && 
                    (!proDto.typeID.HasValue || pro.ProductTypeId == proDto.typeID)
                   
            )
        {
            AddIncludes(pro => pro.ProductBrand!);
            AddIncludes(pro => pro.ProductType!);
            switch (proDto.sortby)
            {
                case "priceAsc":
                    SetOrder(pro => pro.Price);
                    break;
                case "priceDesc":
                    SetOrderByDescending(pro => pro.Price); 
                    break;
                default: 
                    SetOrder(pro => pro.Name!);
                    break;
            }

          
            SetPagination(proDto.pageIndex, proDto.pageSize);
        }

        public ProductsWithTypesandBrands(Expression<Func<Product , bool>> criteria):base(criteria) {

            AddIncludes(pro => pro.ProductBrand!);
            AddIncludes(pro => pro.ProductType!);
        }
  

    }
}
