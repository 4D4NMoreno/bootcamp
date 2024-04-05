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


            _context.Customers.Add(customerToCreate);

            await _context.SaveChangesAsync();

            var customerDTO = new CustomerDTO
            {
                Id = customerToCreate.Id,
                Name = customerToCreate.Name,
                Lastname = customerToCreate.Lastname,
                DocumentNumber = customerToCreate.DocumentNumber,
                Address = customerToCreate.Address,
                Mail = customerToCreate.Mail,
                Phone = customerToCreate.Phone,
           //     CustomerStatus = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), model.CustomerStatus),
                CustomerStatus = nameof(customerToCreate.CustomerStatus),
                Birth = customerToCreate.Birth,
                Bank = new BankDTO()

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
    }
}