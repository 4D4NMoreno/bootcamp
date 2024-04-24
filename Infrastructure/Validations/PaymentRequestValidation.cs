using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class PaymentRequestValidation : AbstractValidator<PaymentRequest>
{
    public PaymentRequestValidation()
    {
        RuleFor(x => x.OriginAccountId)
            .NotNull().WithMessage("Origin account ID cannot be null")
            .NotEmpty().WithMessage("Origin account ID cannot be empty");

        RuleFor(x => x.DocumentNumber)
            .NotEmpty().WithMessage("DocumentNumber cannot be empty");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

    }
}

