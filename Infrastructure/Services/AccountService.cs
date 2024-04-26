using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;

    public AccountService(IAccountRepository repository)
    {
        _repository = repository;
    }
    public async Task<AccountDTO> Add(CreateAccountRequest request)
    {

        return await _repository.Add(request);
    }

    public Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter)
    {
        return _repository.GetFiltered(filter);
    }

    public async Task<AccountDTO> Update(UpdateAccountModel model)
    {
        return await _repository.Update(model);
    }
    public async Task<bool> Delete(int id)
    {
        return await _repository.Delete(id);
    }
}
