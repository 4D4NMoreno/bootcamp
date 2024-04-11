using Core.Models;
using Core.Request;

namespace Core.Interfaces.Repositories;

public interface ICreditCardRepository
{
    Task<CreditCardDTO> Add(CreateCreditCardModel model);

    Task<List<CreditCardDTO>> GetAll();

    Task<bool> BeValidCustomerId(int customerId);

    Task<bool> BeValidCurrencyId(int currencyId);

    Task<CreditCardDTO> Update(UpdateCreditCardModel model);
}
