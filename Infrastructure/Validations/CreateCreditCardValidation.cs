using FluentValidation;
using Core.Models;
using Core.Request;

public class CreateCreditCardModelValidation : AbstractValidator<CreateCreditCardModel>
{
    public CreateCreditCardModelValidation()
    {
        RuleFor(x => x.Designation)
            .NotEmpty().WithMessage("El campo 'Designation' es obligatorio.")
            .MaximumLength(100).WithMessage("El campo 'Designation' no puede tener más de 100 caracteres.");

        RuleFor(x => x.IssueDate)
            .NotEmpty().WithMessage("El campo 'IssueDate' es obligatorio.");

        RuleFor(x => x.ExpirationDate)
            .NotEmpty().WithMessage("El campo 'ExpirationDate' es obligatorio.")
            .GreaterThan(x => x.IssueDate).WithMessage("La fecha de expiración debe ser posterior a la fecha de emisión.");

        RuleFor(x => x.CardNumber)
            .NotEmpty().WithMessage("El campo 'CardNumber' es obligatorio.");

        RuleFor(x => x.CVV)
            .NotEmpty().WithMessage("El campo 'CVV' es obligatorio.");

        RuleFor(x => x.CreditLimit)
            .NotEmpty().WithMessage("El campo 'CreditLimit' es obligatorio.")
            .GreaterThan(0).WithMessage("El límite de crédito debe ser mayor que cero.");

        RuleFor(x => x.AvaibleCredit)
            .NotEmpty().WithMessage("El campo 'AvaibleCredit' es obligatorio.");

        RuleFor(x => x.CurrentDebt)
            .NotEmpty().WithMessage("El campo 'CurrentDebt' es obligatorio.");

        RuleFor(x => x.InterestRate)
            .NotEmpty().WithMessage("El campo 'InterestRate' es obligatorio.");

        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("El campo 'CustomerId' es obligatorio.");


        RuleFor(x => x.CurrencyId)
            .NotEmpty().WithMessage("El campo 'CurrencyId' es obligatorio.");
    }
}