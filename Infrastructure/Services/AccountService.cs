using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;

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
}
