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
        private NorthWindContext _context = new NorthWindContext();
        private static Func<NorthWindContext, int, CustomerSpecial> _getOrderById =
            EF.CompileQuery((NorthWindContext context, int id) =>
                context.Customers.Select(
                    x => new CustomerSpecial
                    {
                        Id = x.CustomerIdentifier,
                        Name = x.CompanyName,
                        Phone = x.Phone, CountryId = x.CountryIdentfier
                    }).FirstOrDefault(x => x.Id == id));

        public CustomerSpecial GetCompiledById(int id)
        {
            return _getOrderById(_context, id);
        }
    }

    public class CustomerSpecial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int? CountryId { get; set; }
    }
}
