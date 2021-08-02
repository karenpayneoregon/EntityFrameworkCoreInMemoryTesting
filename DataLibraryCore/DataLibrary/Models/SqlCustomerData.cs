using System.Collections.Generic;
using System.Linq;
using DataLibraryCore.Contexts;
using DataLibraryCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLibraryCore.Models
{
    public class SqlCustomerData : ICustomer
    {
        public NorthWindContext Context;

        public SqlCustomerData()
        {
            Context = new NorthWindContext();
        }

        public List<Customer> GetAll()
        {
            return Context.Customers.ToList();
        }

        public Customer Add(Customer pCustomers)
        {
            Context.Add(pCustomers);
            return pCustomers;
        }

        public int Commit()
        {
            return Context.SaveChanges();
        }

        public Customer Delete(int id)
        {
            var customer = GetById(id);

            if (customer != null)
            {
                Context.Customers.Remove(customer);
            }

            return customer;

        }

        public Customer GetById(int id)
        {
            return Context.Customers.Find(id);
        }

        public Customer Update(Customer pCustomers)
        {
            var entity = Context.Attach(pCustomers);
            entity.State = EntityState.Modified;
            return pCustomers;
        }
    }
}
