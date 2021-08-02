using System;
using System.Linq;
using System.Threading.Tasks;
using BaseUnitTestProject.Classes;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.EntityFrameworkCore.EF;
using TestBase = BaseUnitTestProject.Base.TestBase;

namespace BaseUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {

        [TestMethod]
        public void BasicReadMockedCustomersData()
        {
            var customers = Context.Customers.OrderBy(x => x.CustomerIdentifier).ToList();

            Assert.AreEqual(customers.Count, 20);
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer}");
            }
        }

        [TestMethod]
        public void BasicReadRealCustomers()
        {
            using var context = new NorthWindContext();
            var customers = context.Customers.OrderBy(x => x.CustomerIdentifier).ToList();

            Assert.AreEqual(customers.Count, 98);
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer}");
            }
        }


        [TestMethod]
        public void AddCustomerTest()
        {
            var contact = new Contact()
            {
                FirstName = "Karen",
                LastName = "Payne"
            };

            Context.Entry(contact).State = EntityState.Added;

            var customer = new Customer()
            {
                CompanyName = "Karen's coffee shop",
                ContactIdentifierNavigation = contact,
                CountryIdentfier = 20
            };

            Context.Entry(customer).State = EntityState.Added;

            var saveChangesCount = Context.SaveChanges();

            Assert.IsTrue(saveChangesCount == 2,
                "Expect one customer and one contact to be added.");

        }

        [TestMethod]
        public async Task CustomersUpdateTest()
        {

            // arrange
            var customer = new Customer()
            {
                ContactIdentifier = 1,
                CustomerIdentifier = 1,
                CompanyName = "ABC",
                CountryIdentfier = 9
            };

            await Context.Customers.AddAsync(customer);
            await Context.SaveChangesAsync();

            // act
            var companyNameNew = "DEF";
            customer.CompanyName = companyNameNew;
            Context.Customers.Update(customer);
            await Context.SaveChangesAsync();

            var customerModified = Context.Customers.Find(customer.CustomerIdentifier);

            // assert
            Assert.IsTrue(customerModified.CompanyName == companyNameNew);


        }



    }
}
