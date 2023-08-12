
using Core.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context)
        {


            if (!context.ProductBrands.Any())
            {
                var productBrandsString = File.ReadAllText("../Infrastructure/Data/Seed/brands.json");
                var productsBrands = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandsString)!;
                context.ProductBrands.AddRange(productsBrands);
            }

            if (!context.ProductTypes.Any())
            {
                var ProductTypesString = File.ReadAllText("../Infrastructure/Data/Seed/types.json");
                var productsTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesString)!;
                context.ProductTypes.AddRange(productsTypes);
            }


            if (!context.Products.Any())
            {
                var productsString = File.ReadAllText("../Infrastructure/Data/Seed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsString)!;
                context.Products.AddRange(products);
            }

            if (context.ChangeTracker.HasChanges()) { await context.SaveChangesAsync(); }

        }
    }
}
