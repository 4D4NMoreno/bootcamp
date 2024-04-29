using Core.Request;
using FluentValidation;
using Infrastructure.Contexts;

namespace Infrastructure.Validations;

public class DepositRequestValidation : AbstractValidator<DepositRequest>
{
    public DepositRequestValidation()
    {

        RuleFor(x => x.DestinationAccountId)
            .NotNull().WithMessage("Destination account ID cannot be null")
            .NotEmpty().WithMessage("Destination account ID cannot be empty");

        RuleFor(x => x.BankId)
            .NotNull().WithMessage("Bank ID cannot be null")
            .NotEmpty().WithMessage("Bank ID cannot be empty");

        RuleFor(x => x.Amount)
                  .NotEmpty().WithMessage("Amount cannot be empty")
                  .PrecisionScale(8, 0, false)
                  .WithMessage("The amount cannot have '.'")
                  .GreaterThan(-1).WithMessage("the transfer amunt cannot be negative");

    }

}




