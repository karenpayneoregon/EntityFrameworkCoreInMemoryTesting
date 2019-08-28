using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.NorthWindOperations
{
    public class Operations
    {
        private readonly NorthWindContext _context = new NorthWindContext();

        private static readonly Func<NorthWindContext, int, CustomerSpecial> _getCustomerSpecialById =
            EF.CompileQuery((NorthWindContext context, int id) =>
                context.Customers.Select(
                    customer => new CustomerSpecial
                    {
                        Id = customer.CustomerIdentifier,
                        Name = customer.CompanyName,
                        Phone = customer.Phone, CountryId = customer.CountryIdentfier
                    }).FirstOrDefault(x => x.Id == id));

        public CustomerSpecial GetCustomerById(int id)
        {
            return _getCustomerSpecialById(_context, id);
        }
    }
}
