using Core.Models;
using Core.Request;

namespace Core.Interfaces.Services;

public interface ICustomerService
{
    Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter);
    Task<CustomerDTO> Add(CreateCustomerModel model);
    Task<CustomerDTO> Update(int Id, UpdateCustomerModel model);
    Task<bool> Delete(int id);
}