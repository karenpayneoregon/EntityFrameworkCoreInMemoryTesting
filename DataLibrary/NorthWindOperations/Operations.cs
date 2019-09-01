using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace DataLibrary.NorthWindOperations
{
    public class Operations
    {
        private readonly NorthWindContext _context = new NorthWindContext();

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
                    }).FirstOrDefault(x => x.Id == customerIdentifier));

        private static readonly Func<NorthWindContext, int, Customer> _getCustomerById =
            EF.CompileQuery((NorthWindContext context, int customerIdentifier) =>
                context.Customers.FirstOrDefault(x => x.CustomerIdentifier == customerIdentifier));
        
        /// <summary>
        /// Get customer by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found customer or null if not found</returns>
        public CustomerSpecial GetCustomerById(int id)
        {
            return _getCustomerSpecialById(_context, id);
        }

        #endregion


    }
}
