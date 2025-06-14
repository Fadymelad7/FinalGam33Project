using EFCore.BulkExtensions;
using Gma33.Core.Entites.OrderModule;
using Gma33.Core.Entites.StoreEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gam33.Repositries.Data
{
    public class StoreContextSeed
    {

        public static async Task DataSeedAsync(StoreContext context)
        {
            if (context.Categories.Count() == 0)
            {

                var CategoryFile = File.ReadAllText("../Gam33.Repositries/Data/DataBaseFilesSeed/CategorySeedGam33.json");
                var Categories = JsonSerializer.Deserialize<List<Category>>(CategoryFile);

                if (Categories is not null && Categories.Count() > 0)
                {
                    foreach (var item in Categories)
                    {
                        context.Set<Category>().Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Products.Any())
            {
                var productFile = await File.ReadAllTextAsync("../Gam33.Repositries/Data/DataBaseFilesSeed/ProductsSeedGam33.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productFile);

                if (products is not null && products.Any())
                {
                    using var transaction = await context.Database.BeginTransactionAsync(); // Start transaction
                    try
                    {
                        // Insert all records using AddRangeAsync
                        await context.Products.AddRangeAsync(products);
                        await context.SaveChangesAsync(); // Save changes to commit the transaction
                        await transaction.CommitAsync(); // Commit transaction
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync(); // Rollback in case of failure
                        throw;
                    }
                }
            }

            if (context.DeliveryMethods.Count() == 0)
            {

                var DeliveryMethodsFile = File.ReadAllText("../Gam33.Repositries/Data/DataBaseFilesSeed/delivery.json");
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsFile);

                if (DeliveryMethods is not null && DeliveryMethods.Count() > 0)
                {
                    foreach (var item in DeliveryMethods)
                    {
                        context.Set<DeliveryMethod>().Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }


        }

    }
    }

