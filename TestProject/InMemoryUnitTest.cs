using System;
using System.Collections.Generic;
using System.Linq;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Classes;
using static Microsoft.EntityFrameworkCore.EF;

namespace TestProject
{
    [TestClass(), TestCategory("InMemory")]
    public class InMemoryUnitTest : TestBase
    {
        private List<Contact> _contactList; 

        [TestInitialize]
        public void Init()
        {

            if (TestContext.TestName == "ContactsLastNameStartsWithTest" )
            {
                _contactList = PrepareContacts();
            }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        #region These test show EF Core logging off

        /// <summary>
        /// Run and view output
        /// </summary>
        [TestMethod]
        public void GetAllCustomersUsingDatabase()
        {
            var ops = new SqlCustomerData();
            var result = ops.GetAll();
        }
        /// <summary>
        /// Run and view output
        /// </summary>
        [TestMethod]
        public void GetAllCustomersICustomerUsingDatabase()
        {
            using (var context = new NorthWindContext())
            {
                var customers = context.Customers.ToList();
            }
        }
        /// <summary>
        /// Run and view output
        /// </summary>
        [TestMethod]
        public void CustomerJoinTest()
        {
            using (var context = new NorthWindContext())
            {
                var customerData = (
                    from customer in context.Customers
                    join contactType in context.ContactType on customer.ContactTypeIdentifier
                        equals contactType.ContactTypeIdentifier
                    join contact in context.Contact on customer.ContactIdentifier equals contact.ContactIdentifier
                    join country in context.Countries on customer.CountryIdentfier equals country.Id
                    select new CustomerEntity
                    {
                        CustomerIdentifier = customer.CustomerIdentifier,
                        CompanyName = customer.CompanyName,
                        ContactIdentifier = customer.ContactIdentifier,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        ContactTypeIdentifier = contactType.ContactTypeIdentifier,
                        ContactTitle = contactType.ContactTitle,
                        Address = customer.Street,
                        City = customer.City,
                        PostalCode = customer.PostalCode,
                        CountryIdentifier = customer.CountryIdentfier,
                        CountyName = country.CountryName
                    }).ToList();

                Assert.IsTrue(customerData.Count == 98);
            }
        }

        #endregion
        /// <summary>
        /// Add new customer to InMemory container
        /// </summary>
        [TestMethod]
        public void AddCustomerTest() 
        {
            using (var context = new NorthWindContext(ContextInMemoryOptions())) 
            {
                var contact = new Contact()
                {
                    FirstName = "Karen",
                    LastName = "Payne"
                };
                context.Entry(contact).State = EntityState.Added;

                var customer = new Customer()
                {
                    CompanyName = "Karen's coffee shop",
                    ContactIdentifierNavigation = contact,
                    CountryIdentfier = 20
                };
                context.Entry(customer).State = EntityState.Added;

                var saveChangesCount = context.SaveChanges();

                Assert.IsTrue(saveChangesCount == 2, 
                    "Expect one customer and one contact to be added.");

            }
        }
        [TestMethod]
        public void CustomersAddRangeTest()
        {
            var customers = CustomersInMemoryList();

            Assert.IsTrue(customers.Count == 20, 
                "Expected to have 20 customer");

            Assert.IsTrue(customers.Any(customer => customer.CustomerIdentifier != 0),
                "Expected all new customers to have a primary key");
        }
        [TestMethod]
        public void CustomersUpdateTest()
        {
            using (var context = new NorthWindContext(ContextInMemoryOptions()))
            {
                var customer = new Customer()
                {
                    ContactIdentifier = 1,
                    CustomerIdentifier = 1,
                    CompanyName = "ABC",
                    CountryIdentfier = 9
                };

                context.Customers.Add(customer);
                context.SaveChanges();

                var companyNameNew = "DEF";
                customer.CompanyName = companyNameNew;
                context.Customers.Update(customer); // <<---new for EF Core
                context.SaveChanges();

                var customerModified = context.Customers.Find(customer.CustomerIdentifier);
                Assert.IsTrue(customerModified.CompanyName == companyNameNew);

            }

        }
        /// <summary>
        /// Test removing a customer
        /// </summary>
        [TestMethod]
        public void RemoveCustomerSetContactNotInUse()
        {
            Assert.IsTrue(DeleteCustomer());
        }

        /// <summary>
        /// Working with Like condition starts-with
        /// </summary>
        [TestMethod]
        public void CustomersLikeConditionStartsWithTest()
        {
            var customers = CustomersInMemoryList();

            var startsWithToken = "Fr%";

            var startsWithResults = customers
                .Where(customer => Functions.Like(
                    customer.CompanyName, 
                    startsWithToken))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 3, 
                "Expected three customers for Like starts with");

        }
        /// <summary>
        /// Working with LIKE condition ends-with with wild card
        /// </summary>
        [TestMethod]
        public void CustomersLikeConditionEndsWithTest()
        {
            var customers = CustomersInMemoryList();

            var endsWithToken = "%_a";

            var endsWithResults = customers
                .Where(customer => Functions.Like(
                    customer.CompanyName,
                    endsWithToken))
                .ToList();

            Assert.IsTrue(endsWithResults.Count == 2,
                "Expected 2 customers for Like ends with");

        }

        /// <summary>
        /// Working with LIKE condition ends-with
        /// </summary>
        [TestMethod]
        public void CustomersLikeConditionContainsTest() 
        {
            var customers = CustomersInMemoryList();

            var containsWithToken = "%en%";

            var startsWithResults = customers
                .Where(customer => Functions.Like(
                    customer.CompanyName,
                    containsWithToken))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 5,
                "Expected 5 customers for Like starts with");

        }
        /// <summary>
        /// Test LIKE starts with
        /// </summary>
        [TestMethod]
        public void ContactsLastNameStartsWithTest()
        {
            var startsWithToken = "Cr%";

            var startsWithResults = _contactList
                .Where(contact => Functions.Like(
                    contact.LastName,
                    startsWithToken))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 4,
                "Expected 4 contacts for Like starts with");

            _contactList = null;
        }

    }
}
