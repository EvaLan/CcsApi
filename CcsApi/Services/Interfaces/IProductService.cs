using CcsApi.Models;

namespace CcsApi.Services.Interfaces
{
    internal interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Product? GetProduct(int id);
        void AddProduct(Product product);
        bool TryUpdateProduct(int id, Product changedProduct);
    }
}
