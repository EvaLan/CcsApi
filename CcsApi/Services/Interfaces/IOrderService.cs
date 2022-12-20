using CcsApi.Models;

namespace CcsApi.Services.Interfaces
{
    internal interface IOrderService
    {
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrdersByCustomer(int customerId);
        Order? GetOrder(int id);
        OrderDetails? GetOrderDetails(int id);
        bool TryAddOrder(Order order, out IDictionary<string, string[]> validationErrors);   
    }
}
