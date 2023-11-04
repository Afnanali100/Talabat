using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class StoreContextDataSeeding
    {
        public static async Task AddSeed(StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrand.Any())
            {
                var data = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);
                if (Brands?.Count() > 0)
                {
                    foreach (var brand in Brands)
                    {
                        await dbcontext.ProductBrand.AddAsync(brand);
                        await dbcontext.SaveChangesAsync();
                    }
                }
            }
            if (!dbcontext.ProductType.Any())
            {
                var data = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(data);
                if (Types?.Count() > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbcontext.ProductType.AddAsync(type);
                        await dbcontext.SaveChangesAsync();
                    }
                }
            }


            if (!dbcontext.Product.Any())
            {
                var data = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(data);
                if (Products?.Count() > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbcontext.Product.AddAsync(product);
                        await dbcontext.SaveChangesAsync();
                    }
                }
            }

        }

    }
}
