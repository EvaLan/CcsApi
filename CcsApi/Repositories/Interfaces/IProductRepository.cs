using CcsApi.Models;

namespace CcsApi.Repositories.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product? GetProduct(int id);
        void AddProduct(Product product);
        bool TryUpdateProduct(int id, Product changedProduct);
    }
}   