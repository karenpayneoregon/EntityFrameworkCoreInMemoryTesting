using System;
using System.Linq;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibraryCore.NorthWindOperations
{
    public class Operations
    {
        private readonly NorthWindContext _context = new ();

        #region For use with large iterations e.g. 100,000 plus (list are not supported yet)

        private static readonly Func<NorthWindContext, int, CustomerSpecial> _getCustomerSpecialById =
            EF.CompileQuery((NorthWindContext context, int customerIdentifier) =>
                context.Customers.Select(
                    customer => new CustomerSpecial
                    {
                        Id = customer.CustomerIdentifier,
                        Name = customer.CompanyName,
                        Phone = customer.Phone,
                        CountryId = customer.CountryIdentfier
                    }).FirstOrDefault(customerSpecial => customerSpecial.Id == customerIdentifier));


        
        /// <summary>
        /// Get customer by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found customer or null if not found</returns>
        public CustomerSpecial GetCustomerById(int id)
        {
            var temp = _context.Customers.ToList();
            var test = _getCustomerSpecialById(_context, id);

            return test;
        }

        #endregion


    }
}
