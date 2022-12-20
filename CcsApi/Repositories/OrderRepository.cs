using CcsApi.Models;
using CcsApi.Repositories.Interfaces;

namespace CcsApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private static readonly Dictionary<int, Order> _orders = new Dictionary<int, Order>();

        public IEnumerable<Order> GetOrders()
        {
            return _orders.Values;
        }
    
        public Order? GetOrder(int id)
        {
            return _orders.GetValueOrDefault(id);
        }

        public void AddOrder(Order order)
        {
            _lock.EnterWriteLock();
            try
            {
                var nextKey = _orders.Keys.Any() ? _orders.Keys.Max() + 1 : 1;
                order.Id = nextKey;
                _orders.Add(nextKey, order);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    
    }
}
