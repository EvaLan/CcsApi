using CcsApi.Models;
using FluentValidation;

namespace CcsApi.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.Products).NotEmpty();
        }
    }
}
