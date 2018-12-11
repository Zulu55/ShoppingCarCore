using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShoppingCarCore.Data.Entities;
using ShoppingCarCore.Models;
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

        public IEnumerable<Order> GetOrders()
        {
            return this.context.Orders
                .Include(o => o.Details)
                .ThenInclude(d => d.Product)
                .OrderBy(o => o.Date);
        }

        public void AddProductToOrder(OrderDetailViewModel model)
        {
            var product = this.GetProduct(model.ProductId);
            var oldOrderDetailTmp = this.context.OrderDetailTmps
                .Where(o => o.Product.Id == model.ProductId)
                .FirstOrDefault();

            if (oldOrderDetailTmp == null)
            {
                this.context.OrderDetailTmps.Add(new OrderDetailTmp
                {
                    Price = product.Price,
                    Product = product,
                    Quantity = model.Quantity
                });
            }
            else
            {
                oldOrderDetailTmp.Quantity += model.Quantity;
                this.context.Update(oldOrderDetailTmp);
            }
        }

        public IEnumerable<OrderDetailTmp> GetOrderDetailTmps()
        {
            return this.context.OrderDetailTmps.Include(o => o.Product);
        }

        public OrderDetailTmp GetOrderDetailTmp(int id)
        {
            return this.context.OrderDetailTmps.Find(id);
        }

        public void DeleteOrderDetailTmp(int id)
        {
            var orderDetailTmp = this.GetOrderDetailTmp(id);
            if (orderDetailTmp != null)
            {
                this.context.OrderDetailTmps.Remove(orderDetailTmp);
            }
        }

        public void ModifyOrderDetailTmp(int id, int quantity)
        {
            var orderDetailTmp = this.GetOrderDetailTmp(id);
            if (orderDetailTmp != null)
            {
                if (orderDetailTmp.Quantity > 1)
                {
                    orderDetailTmp.Quantity += quantity;
                    this.context.Update(orderDetailTmp);
                }
            }
        }

        public void SaveOrder()
        {
            var orderDetailsTmp = this.context.OrderDetailTmps
                .Include(o => o.Product)
                .ToList();

            var order = new Order
            {
                Date = DateTime.UtcNow,
                IsDeliveried = false,
                Details = orderDetailsTmp.Select(odt => new OrderDetail
                {
                    Price = odt.Price,
                    Product = odt.Product,
                    Quantity = odt.Quantity
                }).ToList()
            };

            this.context.Orders.Add(order);
            this.context.OrderDetailTmps.RemoveRange(orderDetailsTmp);
        }
    }
}
