using CcsApi.Models;
using CcsApi.Repositories.Interfaces;
using CcsApi.Services.Interfaces;

namespace CcsApi.Services
{
    internal class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository repository)
        { 
            _customerRepository = repository;
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.AddCustomer(customer);
        }

        public Customer? GetCustomer(int id)
        {
            return _customerRepository.GetCustomer(id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetCustomers();
        }

        public bool TryUpdateCustomer(int id, Customer changedCustomer)
        {
            return _customerRepository.TryUpdateCustomer(id, changedCustomer);
        }
    }
}
