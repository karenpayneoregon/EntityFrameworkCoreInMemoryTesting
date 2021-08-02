using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseUnitTestProject.Base;
using BaseUnitTestProject.Classes;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


// ReSharper disable once CheckNamespace - do not change
namespace BaseUnitTestProject
{
    public partial class MainTest
    {
        private List<Contact> _contactList;

        /// <summary>
        /// Options for in-memory testing
        /// </summary>
        readonly DbContextOptions<NorthWindContext> dbContextOptions = new DbContextOptionsBuilder<NorthWindContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        /// <summary>
        /// Single instance of the DbContext
        /// </summary>
        private NorthWindContext Context;
        protected List<Contact> MockedContacts()
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contacts.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(fileName));
            return contacts;

        }
        [TestInitialize]
        public void Init()
        {
            
            _contactList = MockedData.PrepareContacts();
            
            Context = new NorthWindContext(dbContextOptions);
            
            if (TestContext.TestName != nameof(CustomersUpdateTest))
            {
                Context.Customers.AddRange(MockedData.MockedInMemoryCustomers());
                Context.SaveChanges();
            }

        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
    }

}
