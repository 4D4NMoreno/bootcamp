using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BootcampContext _context;

        public CustomerRepository(BootcampContext context)
        {
            _context = context;
        }
        public async Task<CustomerDTO> Add(CreateCustomerModel model)
        {
            var query = _context.Customers
                .Include(c => c.Bank)
                .AsQueryable();


            var customerToCreate = model.Adapt<Customers>();



            _context.Customers.Add(customerToCreate);

            await _context.SaveChangesAsync();

            var customerBank = await _context.Banks.FindAsync(customerToCreate.BankId);


            var customerDTO = customerToCreate.Adapt<CustomerDTO>();



            return customerDTO;
        }
        public async Task<List<CustomerDTO>> GetFiltered(FilterCustomersModel filter)
        {
            var query = _context.Customers
                .Include(c => c.Bank)
                .AsQueryable();

            if (filter.BirthYearFrom is not null)
            {
                query = query.Where(x =>
                    x.Birth != null &&
                    x.Birth.Value.Year >= filter.BirthYearFrom);
            }

            if (filter.BirthYearTo is not null)
            {
                query = query.Where(x =>
                    x.Birth != null &&
                    x.Birth.Value.Year <= filter.BirthYearTo);
            }

            if (filter.FullName is not null)
            {
                query = query.Where(x => (x.Name + " " + x.Lastname).Contains(filter.FullName));
            }
            if (filter.DocumentNumber is not null)
            {
                query = query.Where(x => x.DocumentNumber.Contains(filter.DocumentNumber));
            }
            if (filter.Mail is not null)
            {
                query = query.Where(x => x.Mail.Contains(filter.Mail));
            }
            if (filter.BankId.HasValue)
            {
                query = query.Where(x => x.BankId == filter.BankId.Value);
            }


            var result = await query.ToListAsync();


            var customerDTOs = result.Adapt<List<CustomerDTO>>();

            return customerDTOs;
        }

        public async Task<CustomerDTO> Update(UpdateCustomerModel model)
        {
            var customerToUpdate = await _context.Customers.FindAsync(model.Id);

            await _context.Banks.FindAsync(model.BankId);

            if (customerToUpdate == null) throw new ArgumentException("El cliente no existe");

            model.Adapt(customerToUpdate);

            _context.Customers.Update(customerToUpdate);

            await _context.SaveChangesAsync();

            var customerDTO = customerToUpdate.Adapt<CustomerDTO>();

            return customerDTO;
        }

            public async Task<CustomerDTO> GetById(int id)
        {
            var Customers = await _context.Customers.FindAsync(id);

            await _context.Banks.ToListAsync();

            if (Customers is null) throw new Exception("Customer not found");

            var customerDTO = Customers.Adapt<CustomerDTO>();

            return customerDTO;
        }
        public async Task<bool> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer is null) throw new Exception("Customer not found");

            _context.Customers.Remove(customer);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }


    }
}