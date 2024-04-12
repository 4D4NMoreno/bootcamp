using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface IAccountService
{
    Task<AccountDTO> Add(CreateAccountModel model);
}
