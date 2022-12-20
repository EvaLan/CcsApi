using CcsApi.Models;
using CcsApi.Repositories.Interfaces;

namespace CcsApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private static readonly Dictionary<int, Product> _products = new Dictionary<int, Product>();

        public Product? GetProduct(int id)
        {
            return _products.GetValueOrDefault(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products.Values;
        }

        public void AddProduct(Product product)
        {
            _lock.EnterWriteLock();
            try
            {
                var nextKey = _products.Keys.Any() ? _products.Keys.Max() + 1 : 1;
                product.Id = nextKey;
                _products.Add(nextKey, product);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public bool TryUpdateProduct(int id, Product changedProduct)
        {
            _lock.EnterWriteLock();
            try
            {
                var product = _products.GetValueOrDefault(id);
                if (product == default)
                    return false;
                product.Name = changedProduct.Name;
                product.Description = changedProduct.Description;
                _products[id] = product;
                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
