using Core.Request;
using Core.Requests;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreateCurrencyModelValidation : AbstractValidator<CreateCurrencyModel>
{
    public CreateCurrencyModelValidation()
    {
        RuleFor(x => x.Name)
       .NotNull().WithMessage("Name cannot be null")
       .NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(x => x.SellValue)
       .NotNull().WithMessage("Sell Value cannot be null")
       .NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(x => x.BuyValue)
      .NotNull().WithMessage("Sell Value cannot be null")
      .NotEmpty().WithMessage("Name cannot be empty");
    }
}
