using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CreditCardService : ICreditCardService
{
    private readonly ICreditCardRepository _repository;

    public CreditCardService(ICreditCardRepository repository)
    {
        _repository = repository;
    }
    public async Task<CreditCardDTO> Add(CreateCreditCardModel model)
    {
  
        if (await _repository.BeValidCustomerId(model.CustomerId) is false)
        {
            throw new NotFoundException("El CustomerId proporcionado no es válido.");
        }

        if (await _repository.BeValidCurrencyId(model.CurrencyId) is false)
        {
            throw new NotFoundException("El CurrencyId proporcionado no es válido.");
        }


        return await _repository.Add(model);
    }
    public async Task<List<CreditCardDTO>> GetAll()
    {
        return await _repository.GetAll();
    }
}
