using CcsApi.Models;
using CcsApi.Repositories.Interfaces;
using CcsApi.Services.Interfaces;

namespace CcsApi.Services
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public Product? GetProduct(int id)
        {
            return _productRepository.GetProduct(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public bool TryUpdateProduct(int id, Product changedProduct)
        {
            return _productRepository.TryUpdateProduct(id, changedProduct);
        }
    }
}
