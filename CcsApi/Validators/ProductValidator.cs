using CcsApi.Models;
using FluentValidation;

namespace CcsApi.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
