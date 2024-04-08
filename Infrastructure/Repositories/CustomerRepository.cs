using Core.Constants;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Request;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var customerToCreate = new Customers
            {
                Name = model.Name,
                Lastname = model.Lastname,
                DocumentNumber = model.DocumentNumber,
                Address = model.Address,
                Mail = model.Mail,
                Phone = model.Phone,
                CustomerStatus = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), model.CustomerStatus),
                Birth = model.Birth
            };

            if (model.BankId != null)
            {
                var bank = await _context.Banks.FindAsync(model.BankId);
                if (bank == null)
                {
                    bank = await _context.Banks.FirstOrDefaultAsync();
                }
                customerToCreate.BankId = bank.Id;
            }

            _context.Customers.Add(customerToCreate);

            await _context.SaveChangesAsync();

            var customerBank = await _context.Banks.FindAsync(customerToCreate.BankId);

            var customerDTO = new CustomerDTO
            {
                Id = customerToCreate.Id,
                Name = customerToCreate.Name,
                Lastname = customerToCreate.Lastname,
                DocumentNumber = customerToCreate.DocumentNumber,
                Address = customerToCreate.Address,
                Mail = customerToCreate.Mail,
                Phone = customerToCreate.Phone,
                CustomerStatus = nameof(customerToCreate.CustomerStatus),
                Birth = customerToCreate.Birth,
                Bank = new BankDTO
                {
                    Id = customerBank.Id,
                    Name = customerBank.Name,
                    Phone = customerBank.Phone,
                    Mail = customerBank.Mail,
                    Address = customerBank.Address
                }
            };

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


            return result.Select(x => new CustomerDTO
            {
                Id = x.Id,
                Name = x.Name,
                Lastname = x.Lastname,
                DocumentNumber = x.DocumentNumber,
                Address = x.Address,
                Mail = x.Mail,
                Phone = x.Phone,
                CustomerStatus = nameof(x.CustomerStatus),
                Birth = x.Birth,
                Bank = new BankDTO
                {
                    Id = x.Bank.Id,
                    Name = x.Bank.Name,
                    Phone = x.Bank.Phone,
                    Mail = x.Bank.Mail,
                    Address = x.Bank.Address
                }
            }).ToList();
        }
        public async Task<CustomerDTO> Update(int Id, UpdateCustomerModel model)
        {
            var customerToUpdate = await _context.Customers.FindAsync(Id);

            if (customerToUpdate == null)
            {
                throw new ArgumentException("El cliente no existe");
            }

            customerToUpdate.Name = model.Name;
            customerToUpdate.Lastname = model.Lastname;
            customerToUpdate.DocumentNumber = model.DocumentNumber;
            customerToUpdate.Address = model.Address;
            customerToUpdate.Mail = model.Mail;
            customerToUpdate.Phone = model.Phone;
            customerToUpdate.CustomerStatus = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), model.CustomerStatus);
            customerToUpdate.Birth = model.Birth;


            if (model.BankId != null)
            {
                var bank = await _context.Banks.FindAsync(model.BankId);
                if (bank == null)throw new Exception("bank was not found");
                customerToUpdate.BankId = bank.Id;
            }

            await _context.SaveChangesAsync();


            var customerBank = await _context.Banks.FindAsync(customerToUpdate.BankId);


            var updatedCustomerDTO = new CustomerDTO
            {
                Id = customerToUpdate.Id,
                Name = customerToUpdate.Name,
                Lastname = customerToUpdate.Lastname,
                DocumentNumber = customerToUpdate.DocumentNumber,
                Address = customerToUpdate.Address,
                Mail = customerToUpdate.Mail,
                Phone = customerToUpdate.Phone,
                CustomerStatus = nameof(customerToUpdate.CustomerStatus),
                Birth = customerToUpdate.Birth,
                Bank = customerBank != null ? new BankDTO
                {
                    Id = customerBank.Id,
                    Name = customerBank.Name,
                    Phone = customerBank.Phone,
                    Mail = customerBank.Mail,
                    Address = customerBank.Address
                } : null
            };

            return updatedCustomerDTO;
        }
        public async Task<bool> Delete(int id)
        {
            var Customers = await _context.Customers.FindAsync(id);

            if (Customers is null) throw new Exception("Customer not found");

            _context.Customers.Remove(Customers);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

    }
}