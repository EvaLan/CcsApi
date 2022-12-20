using CcsApi.Models;

namespace CcsApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer? GetCustomer(int id);
        void AddCustomer(Customer customer);
        bool TryUpdateCustomer(int id, Customer changedCustomer);
    }
}
