using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCarCore.Data.Entities;
using ShoppingCarCore.Models;

namespace ShoppingCarCore.Data
{
    public class RepositoryMock : IRepository
    {
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void AddProductToOrder(OrderDetailViewModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderDetailTmp(int id)
        {
            throw new NotImplementedException();
        }

        public OrderDetailTmp GetOrderDetailTmp(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OrderDetailTmp> GetOrderDetailTmps()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>();

            products.Add(new Product
            {
                Id = 1,
                Name = "First product",
                IsAvailable = true,
                Price = 19.99M
            });

            products.Add(new Product
            {
                Id = 2,
                Name = "Second product",
                IsAvailable = true,
                Price = 9.99M
            });

            products.Add(new Product
            {
                Id = 3,
                Name = "Third product",
                IsAvailable = true,
                Price = 14.99M
            });

            return products;
        }

        public void ModifyOrderDetailTmp(int id, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool ProductExists(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void SaveOrder()
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
