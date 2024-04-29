using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class WithdrawalRequestValidation : AbstractValidator<WithdrawalRequest>
{
    public WithdrawalRequestValidation()
    {
        RuleFor(x => x.OriginAccountId)
            .NotNull().WithMessage("Origin account ID cannot be null")
            .NotEmpty().WithMessage("Origin account ID cannot be empty");

        RuleFor(x => x.BankId)
            .NotNull().WithMessage("Origin account ID cannot be null")
            .NotEmpty().WithMessage("Origin account ID cannot be empty");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount cannot be empty")
            .PrecisionScale(8, 0, false)
            .WithMessage("The amount cannot have '.'")
            .GreaterThan(-1).WithMessage("the transfer amunt cannot be negative");
            


    }
}
