using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCarCore.Data.Entities;

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
    }
}