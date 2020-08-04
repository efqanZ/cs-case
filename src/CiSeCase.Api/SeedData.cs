using System;
using CiSeCase.Core.Models;
using CiSeCase.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CiSeCase.Api
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                // auto migration
                context.Database.Migrate();

                // Seed the user and product data.
                var anyUser = context.Users.AnyAsync(p => !p.Deleted).Result;
                if (!anyUser)
                {
                    context.Users.Add(new User { Id = 1, Deleted = false });
                }

                var anyProduct = context.Products.AnyAsync(p => !p.Deleted).Result;
                if (!anyUser)
                {
                    context.Products.Add(new Product
                    {
                        Id = 1,
                        Deleted = false,
                        StockQuantity = 10
                    });

                    context.Products.Add(new Product
                    {
                        Id = 2,
                        Deleted = false,
                        StockQuantity = 5
                    });
                }
                context.SaveChanges();
            }
        }
    }
}