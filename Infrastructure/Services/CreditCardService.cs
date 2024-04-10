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
        //// Validar el CustomerId
        //if (!BeValidCustomerId(model.CustomerId))
        //{
        //    throw new ArgumentException("El CustomerId proporcionado no es válido.");
        //}

        // Validar el CurrencyId (si es necesario)
        //=============================================================================
        bool nameIsInUse = await _repository.BeValidCustomerId(model.CustomerId);

        if (nameIsInUse)
        {
            throw new BusinessLogicException($"The Customer = {model.CustomerId} is already  use Credit Card");
        }
        //if (!BeValidCurrencyId(model.CurrencyId))
        //{
        //    throw new ArgumentException("La moneda proporcionado no es válido.");
        //}

        return await _repository.Add(model);
    }
    public async Task<List<CreditCardDTO>> GetAll()
    {
        return await _repository.GetAll();
    }
}
