using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
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
        public List<Customers> CustomersList()
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: "Add_Customers_to_database")
                .Options;

            using (var context = new NorthWindContext(options))
            {
                var customers = MockedCustomers();

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

        public List<Contact> PrepareContacts()
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: "Add_Contacts_to_database")
                .Options;

            using (var context = new NorthWindContext(options))
            {
                context.Contact.AddRange(MockedContacts());
                var saveCount = context.SaveChanges();
                return context.Contact.ToList();

            }
        }

    }
}
