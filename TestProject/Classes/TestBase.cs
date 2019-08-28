using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject.Classes
{

    public class TestBase : MockedData
    {
        protected TestContext TestContextInstance;
        public TestContext TestContext
        {
            get => TestContextInstance;
            set
            {
                TestContextInstance = value;
                TestResults.Add(TestContext);
            }
        }

        public static IList<TestContext> TestResults;

        /// <summary>
        /// Create a in memory representation of the customer table which does not
        /// populate all properties.
        /// </summary>
        /// <returns></returns>
        public List<Customer> CustomersInMemoryList()
        {

            using (var context = new NorthWindContext(ContextInMemoryOptions()))
            {
                var customers = MockedInMemoryCustomers();

                /*
                 * Using InMemory default values are not done so modified date must be done here
                 */
                foreach (var customer in customers) 
                {
                    customer.ModifiedDate = DateTime.Now;
                }

                context.Customers.AddRange(customers);

                context.SaveChanges();

                return customers;
            }
        }
        /// <summary>
        /// Setup in memory contacts.
        /// Note context.Contact.Clear(), without this had some
        /// strange things happen, will look deeper into this.
        /// </summary>
        /// <returns></returns>
        public List<Contact> PrepareContacts()
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: "Add_Contacts_to_database")
                .Options;

            using (var context = new NorthWindContext(options))
            {
                context.Contact.Clear();
                context.SaveChanges();

                context.Contact.AddRange(MockedContacts());
                context.SaveChanges();

                return context.Contact.ToList();

            }
        }

         




        /// <summary>
        /// Remove customer, mark contact as not in use
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// All mockups must be saved before performing
        /// the customer remove operation.
        /// </remarks>
        public bool DeleteCustomer()
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: "Remove_Customer_to_database")
                .Options;

            using (var context = new NorthWindContext(options))
            {
                /*
                 * Mock-up tables
                 */
                context.Customers.AddRange(MockedInMemoryCustomers());
                context.Contact.AddRange(PrepareContacts());
                context.SaveChanges();

                /*
                 * Find customer and contact
                 */
                var customer = context.Customers.FirstOrDefault(
                    cust => cust.CompanyName == "Around the Horn");

                var contact = context.Contact.FirstOrDefault(
                    con => con.ContactIdentifier == customer.ContactIdentifier);

                if (contact != null)
                {
                    var contactIdentifier = contact.ContactIdentifier;

                    context.Entry(customer).State = EntityState.Modified;

                    context.SaveChanges();

                    context.Customers.Remove(customer);
                    contact.InUse = false;

                    context.SaveChanges();

                    customer = context.Customers.FirstOrDefault(
                        cust => cust.CompanyName == "Around the Horn"); 

                    contact = context.Contact.FirstOrDefault(
                        con => con.ContactIdentifier == contactIdentifier);
                }

                return customer == null && contact.InUse == false;
            }
        }

        public DbContextOptions<NorthWindContext> ContextInMemoryOptions()
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return options;
        }
        public DbContextOptions<NorthWindContext> ContextSqlLiteOptions(SqliteConnection pConnection)  
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseSqlite(pConnection)
                .EnableSensitiveDataLogging()
                .Options;

            return options;
        }

    }
}
