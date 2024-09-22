using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data
{
    public static class SeedG01DbContext
    {
        public static async Task SeedAsync(G01DbContext DbContactor)
        {
            #region ProductBrands
            if (!DbContactor.Set<ProductBrand>().Any())
            {

                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brand = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brand?.Count > 0)
                {
                    foreach (var item in Brand)
                    {
                        await DbContactor.Set<ProductBrand>().AddAsync(item);
                    }
                    await DbContactor.SaveChangesAsync();
                }
            }
            #endregion

            #region ProductTypes
            if (!DbContactor.Set<ProductType>().Any())
            {

                var TypesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Type = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (Type?.Count > 0)
                {
                    foreach (var item in Type)
                    {
                        await DbContactor.Set<ProductType>().AddAsync(item);
                    }
                    await DbContactor.SaveChangesAsync();
                }
            }
            #endregion

            #region Product
            if (!DbContactor.Set<Product>().Any())
            {

                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var product = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (product?.Count > 0)
                {
                    foreach (var item in product)
                    {
                        await DbContactor.Set<Product>().AddAsync(item);
                    }
                    await DbContactor.SaveChangesAsync();
                }
            }
            #endregion
        }
    }
}
