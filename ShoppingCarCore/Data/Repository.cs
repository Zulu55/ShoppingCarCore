using Microsoft.Extensions.Logging;
using ShoppingCarCore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCarCore.Data
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<Repository> logger;

        public Repository(ApplicationDbContext context, ILogger<Repository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IEnumerable<Product> GetProducts()
        {
            return this.context.Products.OrderBy(p => p.Name);
        }

        public Product GetProduct(int id)
        {
            return this.context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public void AddProduct(Product product)
        {
            this.context.Products.Add(product);
        }

        public async Task<bool> SaveAllAsync()
        {
            try
            {
                return await this.context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                this.logger.LogError($"Something horrible has happened: {ex}");
                return false;
            }
        }

        public void UpdateProduct(Product product)
        {
            this.context.Update(product);
        }

        public void RemoveProduct(Product product)
        {
            this.context.Remove(product);
        }

        public bool ProductExists(int id)
        {
            return this.context.Products.Any(p => p.Id == id);
        }
    }
}
