﻿using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;

namespace Infrastructure.Services;

public class ProductRequestService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductRequestService(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<ProductRequestDTO> Add(CreateProductRequest request)
    {

        return await _repository.Add(request);
    }
}
