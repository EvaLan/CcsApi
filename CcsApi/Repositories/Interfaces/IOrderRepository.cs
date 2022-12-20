using CcsApi.Models;

namespace CcsApi.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();      
        Order? GetOrder(int id);
        void AddOrder(Order order);      
    }
}
