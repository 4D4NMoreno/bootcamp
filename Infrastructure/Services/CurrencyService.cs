using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;

namespace Infrastructure.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _repository;

    public CurrencyService(ICurrencyRepository repository)
    {
        _repository = repository;
    }

    public async Task<CurrencyDTO> Add(CreateCurrencyModel model)
    {

        return await _repository.Add(model);
    }
}
