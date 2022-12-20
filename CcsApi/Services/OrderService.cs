using CcsApi.Models;
using CcsApi.Repositories.Interfaces;
using CcsApi.Services.Interfaces;

namespace CcsApi.Services
{
    internal class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;
        private IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        public Order? GetOrder(int id)
        {
            return _orderRepository.GetOrder(id);
        }

        public OrderDetails? GetOrderDetails(int id)
        {
            var order = _orderRepository.GetOrder(id);
            if (order == default) return default;
            var customer = _customerRepository.GetCustomer(order.CustomerId);
            if (customer == default) return default;
            var products = _productRepository.GetProducts().Where(product => order.Products.ContainsKey(product.Id))
                .Select(product => new OrderDetails.ProductEntry
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = order.Products[product.Id]
                }).ToList();
            return new OrderDetails
            {
                Id = id,
                CustomerName = customer.Name,
                Products = products
            };
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetOrders();
        }

        public IEnumerable<Order> GetOrdersByCustomer(int customerId)
        {
            return _orderRepository.GetOrders().Where(order => order.CustomerId == customerId);
        }

        public bool TryAddOrder(Order order, out IDictionary<string, string[]> validationErrors)
        {
            validationErrors = new Dictionary<string, string[]>();
            var customer = _customerRepository.GetCustomer(order.CustomerId);
            if (customer == default)
            {
                validationErrors.Add(nameof(Order.CustomerId), new string[] { "Customer not found" });
                return false;
            }
            foreach (var productId in order.Products.Keys)
            {
                if (_productRepository.GetProduct(productId) is not Product product)
                {
                    validationErrors.Add(nameof(Order.Products), new string[] { $"Product with ID {productId} not found" });
                    return false;
                }
            }
            _orderRepository.AddOrder(order);
            return true;
        }
    }
}
