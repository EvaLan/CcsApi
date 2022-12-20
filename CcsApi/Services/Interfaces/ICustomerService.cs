using CcsApi.Models;

namespace CcsApi.Services.Interfaces
{
    internal interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        Customer? GetCustomer(int id);
        void AddCustomer(Customer customer);
        bool TryUpdateCustomer(int id, Customer changedCustomer);
    }
}
