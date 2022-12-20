using CcsApi.Models;
using FluentValidation;

namespace CcsApi.Validators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {         
            RuleFor(x => x.Name).NotEmpty();           
        }
    }
}
