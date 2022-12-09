using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class DbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
        {
            try
            {  
                if (!dbContext.Brands.Any())
                {
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<Brand>>(brandsData);
                    foreach (var item in brands)
                    {
                        dbContext.Brands.Add(item);
                    }
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                    foreach (var item in categories)
                    {
                        dbContext.Categories.Add(item);
                    }
                    await dbContext.SaveChangesAsync();
                }

                if (!dbContext.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                    {
                        dbContext.Products.Add(item);
                    }
                    await dbContext.SaveChangesAsync();
                }

            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DbContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}