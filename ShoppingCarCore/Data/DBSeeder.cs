using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using ShoppingCarCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCarCore.Data
{
    public class DBSeeder
    {
        private readonly ApplicationDbContext context;

        public DBSeeder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            this.context.Database.EnsureCreated();

            if(!this.context.Products.Any())
            {
                this.context.Products.Add(new Product
                {
                    IsAvailable = true,
                    Name = "First product",
                    Price = 19.99M
                });

                this.context.Products.Add(new Product
                {
                    IsAvailable = true,
                    Name = "Second product",
                    Price = 9.99M
                });

                this.context.Products.Add(new Product
                {
                    IsAvailable = true,
                    Name = "Third product",
                    Price = 14.99M
                });

                await this.context.SaveChangesAsync();
            }
        }
    }
}