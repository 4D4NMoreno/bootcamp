using Core.Request;
using Core.Requests;
using FluentValidation;

namespace Infrastructure.Validations;
public class CreatePromotionModelValidation : AbstractValidator<CreatePromotionModel>
{
    public CreatePromotionModelValidation()
    {
        RuleFor(x => x.EnterpriseIds)
       .NotNull().WithMessage("EnterpriseIds cannot be null")
       .NotEmpty().WithMessage("EnterpriseIds cannot be empty");

        RuleFor(x => x.Discount)
       .NotEmpty().WithMessage("CustomerId cannot be empty")
       .GreaterThan(0).WithMessage("The discount must be greater than 0.");

        RuleFor(x => x.Name)
       .NotEmpty().WithMessage("Name cannot be empty");

        RuleFor(x => x.Start)
      .NotEmpty().WithMessage("Start date cannot be empty");

        RuleFor(x => x.End)
            .NotEmpty().WithMessage("Start date cannot be empty")
            .GreaterThan(x => x.Start).WithMessage("The end date must be later than the date the promotion starts.");

    }

}
