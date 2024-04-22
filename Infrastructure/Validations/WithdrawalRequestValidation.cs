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
            .GreaterThan(0).WithMessage("Amount must be greater than 0");


    }
}
