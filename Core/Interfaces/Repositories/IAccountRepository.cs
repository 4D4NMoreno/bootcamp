﻿using Core.Models;
using Core.Request;
using Core.Requests;

namespace Core.Interfaces.Repositories;

public interface IAccountRepository
{
    Task<AccountDTO> Add(CreateAccountRequest request);
    Task<List<AccountDTO>> GetFiltered(FilterAccountModel filter);
    Task<AccountDTO> Update(UpdateAccountModel model);

    Task<bool> Delete(int id);
}
