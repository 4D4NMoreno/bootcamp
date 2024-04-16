using Core.Constants;
using Core.Request;
using FluentValidation;

namespace Infrastructure.Validations;

public class CreateCustomerModelValidation : AbstractValidator<CreateCustomerModel>
{
    public CreateCustomerModelValidation()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("El nombre es obligatorio.")
            .NotEmpty().WithMessage("El nombre no puede estar vacío.");

        RuleFor(x => x.BankId)
            .NotNull().WithMessage("El BankId es obligatorio.");

        RuleFor(x => x.DocumentNumber)
            .NotNull().WithMessage("Document cannot be null")
            .NotEmpty().WithMessage("Document canno be empty");

        RuleFor(x => x.Mail)
            .EmailAddress();

    }
}
