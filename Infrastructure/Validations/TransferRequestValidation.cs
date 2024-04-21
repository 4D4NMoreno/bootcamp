using FluentValidation;
using Core.Request;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Infrastructure.Contexts;

namespace Infrastructure.Validations
{
  public class TransferRequestValidation : AbstractValidator<TransferRequest>
  {
    private readonly BootcampContext _context;

    public TransferRequestValidation(BootcampContext context)
    {
       _context = context;

       RuleFor(x => x.OriginAccountId)
             .NotNull().WithMessage("Origin account ID cannot be null")
             .NotEmpty().WithMessage("Origin account ID cannot be empty");

       RuleFor(x => x.Amount)
             .GreaterThan(0).WithMessage("Amount must be greater than 0");
       

       RuleFor(x => x)
       .Custom((request, context) =>
       {
          var originAccount = _context.Accounts
            .Include(a => a.Customer)
            .ThenInclude(c => c.Bank)
            .FirstOrDefault(a => a.Id == request.OriginAccountId);



           if (request.DestinationBank.HasValue &&
                        originAccount.Customer.BankId != request.DestinationBank)

           {
              if (string.IsNullOrEmpty(request.DestinationAccountNumber))

              {
                 context.AddFailure
                 ("Destination account number is required when transferring between different banks.");
              }

              if (string.IsNullOrEmpty(request.DestinationDocumentNumber))

              {
                 context.AddFailure
                 ("Destination document number is required when transferring between different banks.");
              }
               
           }
      });
    }
  }
}
