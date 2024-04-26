using Core.Request;
using Core.Requests;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreateAccountModelValidation : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountModelValidation()
    {
        RuleFor(x => x.CurrencyId)
       .NotNull().WithMessage("CurrencyId cannot be null")
       .NotEmpty().WithMessage("CurrencyId cannot be empty");

        RuleFor(x => x.CustomerId)
       .NotNull().WithMessage("CustomerId cannot be null")
       .NotEmpty().WithMessage("CustomerId cannot be empty");

    }
}
