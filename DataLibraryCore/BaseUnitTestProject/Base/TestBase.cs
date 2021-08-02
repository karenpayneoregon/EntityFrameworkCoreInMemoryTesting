using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using BaseUnitTestProject.Classes;
using Microsoft.EntityFrameworkCore;

namespace BaseUnitTestProject.Base
{
    public class TestBase
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
        public List<Customer> CustomersInMemoryList()
        {
            using var context = new NorthWindContext(ContextInMemoryOptions());
            var customers = MockedData.MockedInMemoryCustomers();

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

        public DbContextOptions<NorthWindContext> ContextInMemoryOptions()
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            return options;
        }


    }
}
