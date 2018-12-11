using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCarCore.Data.Entities;
using ShoppingCarCore.Models;

namespace ShoppingCarCore.Data
{
    public interface IRepository
    {
        void AddProduct(Product product);

        Product GetProduct(int id);

        IEnumerable<Product> GetProducts();

        void RemoveProduct(Product product);

        Task<bool> SaveAllAsync();

        void UpdateProduct(Product product);

        bool ProductExists(int id);

        IEnumerable<Order> GetOrders();

        void AddProductToOrder(OrderDetailViewModel model);

        IEnumerable<OrderDetailTmp> GetOrderDetailTmps();

        OrderDetailTmp GetOrderDetailTmp(int id);

        void DeleteOrderDetailTmp(int id);

        void ModifyOrderDetailTmp(int id, int quantity);

        void SaveOrder();
    }
}