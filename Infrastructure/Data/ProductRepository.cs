using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _storeContext;
        public ProductRepository(StoreContext storeContext)
        {

            _storeContext = storeContext;

        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync()
        {
            return await _storeContext.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProdcutsAsync()
        {
            return await _storeContext.Products
                .Include(prod => prod.ProductBrand)
                .Include(prod => prod.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllTypesAsync()
        {
            return await _storeContext.ProductTypes.ToListAsync();
        }

        public async Task<Product?> GetProductByID(int id)
        {
            return await _storeContext.Products
                .Include(prod => prod.ProductBrand)
                .Include(prod => prod.ProductType)
                .FirstOrDefaultAsync(prod => prod.Id == id);
        }
    }
}
