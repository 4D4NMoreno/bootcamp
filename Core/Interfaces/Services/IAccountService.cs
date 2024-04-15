using Core.Models;
using Core.Request;
using Core.Requests;

namespace Core.Interfaces.Services;

public interface IAccountService
{
    Task<AccountDTO> Add(CreateAccountRequest request);
    Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter);
}
