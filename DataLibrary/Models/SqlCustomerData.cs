using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkCoreLikeLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary.Models
{
    public class SqlCustomerData : ICustomer
    {
        private readonly NorthWindContext _context;

        public SqlCustomerData(NorthWindContext pNorthWindContext)
        {
            _context = pNorthWindContext;
        }

        public Customer Add(Customer pCustomers)
        {
            _context.Add(pCustomers);
            return pCustomers;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Customer Delete(int id)
        {
            var customer = GetById(id);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            return customer;

        }

        public Customer GetById(int id)
        {
            return _context.Customers.Find(id);
        }

        public Customer Update(Customer pCustomers)
        {
            var entity = _context.Attach(pCustomers);
            entity.State = EntityState.Modified;
            return pCustomers;
        }
    }
}
