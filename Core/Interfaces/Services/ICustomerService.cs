using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface ICustomerService
{
    Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter);
    Task<CustomerDTO> Add(CreateCustomerModel model);
}