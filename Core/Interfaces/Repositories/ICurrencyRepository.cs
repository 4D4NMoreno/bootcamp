using Core.Models;
using Core.Request;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface ICurrencyRepository
{
    Task<CurrencyDTO> Add(CreateCurrencyModel model);

    Task<List<CurrencyDTO>> GetFiltered(FilterCurrencyModel filter);

    Task<CurrencyDTO> GetById(int id);

    Task<bool> Delete(int id);

    Task<CurrencyDTO> Update(UpdateCurrencyModel model);
}
