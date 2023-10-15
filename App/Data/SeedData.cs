using App.Data;
using App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Validate
                if (context.Product == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                // If there are any existing products, return to avoid seeding
                if (context.Product.Any())
                {
                    return; // Database has already been seeded
                }

                var random = new Random();

                // Generate 100 sample products with random quantities and prices and add them to the database
                for (int i = 1; i <= 100; i++)
                {
                    context.Product.Add(new Product
                    {
                        Name = $"Product {i}",
                        Description = $"Description for Product {i}",
                        Quantity = random.Next(0, 1001), // Random quantity between 0 and 1000
                        Price = Math.Round((decimal)(random.NextDouble() * (100 - 0.01) + 0.01), 2) // Random price between 0.01 and 100
                    });
                }

                // Save changes to the database
                context.SaveChanges();
            }
        }
    }
}
