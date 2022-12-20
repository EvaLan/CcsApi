using CcsApi.Models;
using CcsApi.Services.Interfaces;
using FluentValidation;

namespace CcsApi.ApiEndpoints
{
    internal static class ProductApi
    {
        private const string Path = "/ccs/products";
        internal static void RegisterProductApi(WebApplication app)
        {
            app.MapGet(Path, (IProductService productService) =>
            {
                return Results.Ok(productService.GetProducts().ToList());
            });

            app.MapGet(Path + "/{id:int}", (int id, IProductService productService) =>
            {
                return productService.GetProduct(id) is Product product
                    ? Results.Ok(product)
                    : Results.NotFound();
            });

            app.MapPost(Path, (Product product, IProductService productService, IValidator<Product> productValidator) =>
            {
                var validationResult = productValidator.Validate(product);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                productService.AddProduct(product);
                return Results.Created($"{Path}/{product.Id}", product);
            });

            app.MapPut(Path + "/{id:int}", (int id, Product product, IProductService productService, IValidator<Product> productValidator) =>
            {
                var validationResult = productValidator.Validate(product);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
                return productService.TryUpdateProduct(id, product) 
                    ? Results.NoContent()
                    : Results.NotFound();
            });
        }
    }
}
