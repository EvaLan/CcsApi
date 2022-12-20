using CcsApi.Models;
using CcsApi.Services.Interfaces;
using FluentValidation;

namespace CcsApi.ApiEndpoints
{
    public static class OrderApi
    {
        private const string Path = "/ccs/orders";     
        public static void RegisterOrderApi(WebApplication app)
        {
            app.MapGet(Path, (IOrderService orderService) =>
            {
                return Results.Ok(orderService.GetOrders().ToList());
            });

            app.MapGet(Path + "/{id:int}", (int id, IOrderService orderService) =>
            {
                return orderService.GetOrder(id) is Order order
                    ? Results.Ok(order)
                    : Results.NotFound();
            });

            app.MapGet(Path + "/details/{id:int}", (int id, IOrderService orderService) =>
            {
                return orderService.GetOrderDetails(id) is OrderDetails orderDetails
                    ? Results.Ok(orderDetails)
                    : Results.NotFound();
            });

            app.MapGet(Path + "/byCustomer/{id:int}", (int customerId, IOrderService orderService) =>
            {
                return Results.Ok(orderService.GetOrdersByCustomer(customerId).ToList());
            });           

            app.MapPost(Path, (Order order, IOrderService orderService, IValidator<Order> orderValidator) =>
            {
                var validationResult = orderValidator.Validate(order);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                return orderService.TryAddOrder(order, out var validationErrors) 
                    ? Results.Created($"{Path}/{order.Id}", order)
                    : Results.ValidationProblem(validationErrors);
            });
        }
    }
}
