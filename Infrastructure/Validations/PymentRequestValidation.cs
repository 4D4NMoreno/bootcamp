using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class PymentRequestValidation : AbstractValidator<PaymentRequest>
{
    public PymentRequestValidation()
    {
        RuleFor(x => x.OriginAccountId)
            .NotNull().WithMessage("Origin account ID cannot be null")
            .NotEmpty().WithMessage("Origin account ID cannot be empty");

        RuleFor(x => x.DocumentNumber)
            .NotNull().WithMessage("DocumentNumber cannot be null");

        RuleFor(x => x.Description)
            .NotNull().WithMessage("Description cannot be null")
            .NotEmpty().WithMessage("Description canno be empty");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0");

    }
}

