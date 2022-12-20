using CcsApi.Models;
using CcsApi.Repositories.Interfaces;

namespace CcsApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private static readonly Dictionary<int, Customer> _customers = new Dictionary<int, Customer>();

        public void AddCustomer(Customer customer)
        {
            _lock.EnterWriteLock();
            try
            {
                var nextKey = _customers.Keys.Any() ? _customers.Keys.Max() + 1 : 1;
                customer.Id = nextKey;
                _customers.Add(nextKey, customer);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Customer? GetCustomer(int id)
        {
            return _customers.GetValueOrDefault(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customers.Values;
        }

        public bool TryUpdateCustomer(int id, Customer changedCustomer)
        {
            _lock.EnterWriteLock();
            try
            {
                var customer = _customers.GetValueOrDefault(id);
                if (customer == default)
                    return false;
                customer.Name = changedCustomer.Name;
                _customers[id] = customer;
                return true;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
