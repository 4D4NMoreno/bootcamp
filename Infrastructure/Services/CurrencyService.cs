﻿using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Repositories;

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

    public async Task<List<CurrencyDTO>> GetFiltered(FilterCurrencyModel filter)
    {
        return await _repository.GetFiltered(filter);
    }
    public async Task<CurrencyDTO> GetById(int id)
    {
        return await _repository.GetById(id);
    }
    public async Task<bool> Delete(int id)
    {
        return await _repository.Delete(id);
    }
    public async Task<CurrencyDTO> Update(UpdateCurrencyModel model)
    {
        return await _repository.Update(model);
    }
}
