using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseUnitTestProject.Base;
using BaseUnitTestProject.Classes;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;


// ReSharper disable once CheckNamespace - do not change
namespace BaseUnitTestProject
{
    public partial class MainTest
    {
        private List<Contact> _contactList;

        readonly DbContextOptions<NorthWindContext> dbContextOptions = new DbContextOptionsBuilder<NorthWindContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).EnableSensitiveDataLogging().Options;

        private NorthWindContext Context; 

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
