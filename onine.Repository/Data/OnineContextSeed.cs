using onine.core.Entites;
using System.Text.Json;

namespace onine.Repository.Data
{
    public class OnineContextSeed
    {
        public static async Task SeedAsync(Oninecontext dbcontext)
        {
            if (!dbcontext.Set<ProductBrand>().Any())
            {
                var BrandsData = File.ReadAllText("../onine.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {
                    foreach (var brand in Brands)
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(brand);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.Set<ProductType>().Any())
            {
                var TypesData = File.ReadAllText("../onine.Repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (Types?.Count > 0)
                {
                    foreach (var type in Types)
                    {
                        await dbcontext.Set<ProductType>().AddAsync(type);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.Set<Product>().Any())
            {
                var ProductsData = File.ReadAllText("../onine.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await dbcontext.Set<Product>().AddAsync(product);
                    }

                    await dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}