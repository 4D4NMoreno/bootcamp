using Core.Models;
using Core.Request;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface ICurrencyRepository
{
    Task<CurrencyDTO> Add(CreateCurrencyModel model);
}
