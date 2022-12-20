using CcsApi.Models;
using CcsApi.Services.Interfaces;
using FluentValidation;

namespace CcsApi.ApiEndpoints
{
    public static class CustomerApi
    {
        private const string Path = "/ccs/customers";  

        public static void RegisterCustomerApi(WebApplication app)
        {
            app.MapGet(Path, (ICustomerService customerService) =>
            {
                return Results.Ok(customerService.GetCustomers().ToList());
            });

            app.MapGet(Path + "/{id:int}", (int id, ICustomerService customerService) =>
            {
                return customerService.GetCustomer(id) is Customer customer
                    ? Results.Ok(customer)
                    : Results.NotFound();
            });

            app.MapPost(Path, (Customer customer, ICustomerService customerService, IValidator<Customer> customerValidator) =>
            {
                var validationResult = customerValidator.Validate(customer);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                customerService.AddCustomer(customer);
                return Results.Created($"{Path}/{customer.Id}", customer);
            });

            app.MapPut(Path + "/{id:int}", (int id, Customer customer, ICustomerService customerService, IValidator<Customer> customerValidator) =>
            {
                var validationResult = customerValidator.Validate(customer);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                return customerService.TryUpdateCustomer(id, customer)
                    ? Results.NoContent()
                    : Results.NotFound();
            });
        }
    }
}
