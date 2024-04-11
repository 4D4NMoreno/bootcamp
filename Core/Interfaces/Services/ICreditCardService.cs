using Core.Models;
using Core.Request;


namespace Core.Interfaces.Services;

public interface ICreditCardService
{
    Task<CreditCardDTO> Add(CreateCreditCardModel model);

    Task<List<CreditCardDTO>> GetAll();

    Task<CreditCardDTO> Update(UpdateCreditCardModel model);
}
