﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllProdcutsAsync();

        Task<Product?> GetProductByID(int id);

        Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetAllTypesAsync();

    }
}
