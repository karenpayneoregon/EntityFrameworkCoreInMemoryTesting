using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseUnitTestProject.Base;
using BaseUnitTestProject.Classes;
using BaseUnitTestProject.Classes.NorthClasses;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using DataLibraryCore.NorthWindOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.EntityFrameworkCore.EF;
using CustomerEntity = DataLibraryCore.Models.CustomerEntity;
using TestBase = BaseUnitTestProject.Base.TestBase;

namespace BaseUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {

        [TestMethod]
        [TestTraits(Trait.InMemoryTesting)]
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
        [TestTraits(Trait.ReadDataTesting)]
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
        [TestTraits(Trait.InMemoryTesting_CRUD)]
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
        [TestTraits(Trait.InMemoryTesting_CRUD)]
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

        /// <summary>
        /// This test uses StartsWith as apposed to Functions.Like because EF Core does not support client-evaluation.
        /// Which means that in-memory testing can not be used.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.InMemoryTesting)]
        public void ContactsLastNameStartsWithLocal()
        {
            var startsWithToken = "Cr";
            var startsWithResults = _contactList
                .Where(contact => contact.LastName.StartsWith(startsWithToken, StringComparison.OrdinalIgnoreCase))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 4,
                "Expected 4 contacts for Like starts with");

            _contactList = null;
        }

        /// <summary>
        /// Unlike <see cref="ContactsLastNameStartsWithLocal"/> since reading from the live database there is not client side evaluation
        /// and Functions.Like works.
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.ReadDataLike)]
        public void ContactsLastNameStartsWithReal()
        {
            using var context = new NorthWindContext();

            var startsWithToken = "Cr%";

            var startsWithResults = context.Contact
                .Where(contact => Functions.Like(
                    contact.LastName,
                    startsWithToken))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 4,
                "Expected 4 contacts for Like starts with");

        }
        
        /// <summary>
        /// Simple in-memory test for crud
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.InMemoryTesting_CRUD)]
        public void RemoveCustomerSetContactNotInUse()
        {
            const string findCompanyName = "Around the Horn";

            Context.Contact.AddRange(MockedContacts());
            Context.SaveChanges();

            var singleCustomer = Context.Customers.FirstOrDefault(cust => cust.CompanyName == findCompanyName);
            var singleContact = Context.Contact.FirstOrDefault(con => con.ContactIdentifier == singleCustomer.ContactIdentifier);

            if (singleContact is not null)
            {
                var contactIdentifier = singleContact.ContactIdentifier;
                Context.Entry(singleCustomer!).State = EntityState.Modified;

                Context.SaveChanges();

                Context.Customers.Remove(singleCustomer);
                singleContact.InUse = false;

                Context.SaveChanges();

                singleCustomer = Context.Customers.FirstOrDefault(cust => cust.CompanyName == findCompanyName);

                singleContact = Context.Contact.FirstOrDefault(
                    con => con.ContactIdentifier == contactIdentifier);


                Assert.IsTrue(singleCustomer == null && singleContact.InUse == false);

            }


        }

        /// <summary>
        /// Live data with Projection
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.ReadDataTesting)]
        public void CustomerJoinTest()
        {
            using var context = new NorthWindContext();
            
            List<CustomerEntity> customerData = 
            (
                from customer in context.Customers
                join contactType in context.ContactType on customer.ContactTypeIdentifier equals contactType.ContactTypeIdentifier
                join contact in context.Contact on customer.ContactIdentifier equals contact.ContactIdentifier
                join country in context.Countries on customer.CountryIdentfier equals country.Id
                /*
                 * Projection
                 */
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
                    }
                ).ToList();

            Assert.IsTrue(customerData.Count == 98);
        }

        /// <summary>
        /// Remove Ignore to create new json files.
        /// </summary>
        /// <returns>N/A</returns>
        [TestMethod]
        [Ignore]
        [TestTraits(Trait.JsonCreate)]
        public async Task CreateJson()
        {
            await Operations.SerializeModelsToJson();
        }

        /// <summary>
        /// Devoid of Entity Framework, shows plain Jane LINQ read from files
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.NoInMemoryTesting)]
        public void ReadCustomersFromJson()
        {
            var joinList = MockedData1.ReadCustomersWithJoins();
            Assert.IsTrue(joinList.All(customerEntity => customerEntity.Contact is not null));
            Assert.IsTrue(joinList.All(customerEntity => customerEntity.ContactTypeNavigation is not null));
            Assert.IsTrue(joinList.All(customerEntity => customerEntity.LastName is not null));

        }

        /// <summary>
        /// Test navigation properties with in-memory data
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.InMemoryTestingRelations)]
        public void LoadingRelations()
        {

            var singleCustomer = Context.Customers
                .Include(customer => customer.CountryIdentfierNavigation)
                .Include(customer => customer.ContactIdentifierNavigation)
                .FirstOrDefault(customer => customer.CustomerIdentifier == 5);
            
            Assert.AreEqual(singleCustomer.CompanyName, "Blauer See Delikatessen");
            Assert.AreEqual(singleCustomer.CountryIdentfierNavigation.CountryName, "Germany");
            Assert.AreEqual(singleCustomer.ContactIdentifierNavigation.FirstName, "Hanna");
            Assert.AreEqual(singleCustomer.ContactIdentifierNavigation.LastName, "Moos");
        }

        /// <summary>
        /// Create a single customer
        /// Modified date is not auto generated as in memory testing does not provide this functionality
        /// </summary>
        [TestMethod]
        [TestTraits(Trait.InMemoryTestingCreateCustomer)]
        public void CreateCustomer()
        {
            var customer = MockSingleCustomer();
            
            Assert.IsTrue(customer.CustomerIdentifier == 1);
            Assert.IsTrue(customer.CompanyName == "Karen's Coffee shop");
            Assert.IsNotNull(customer.ContactIdentifierNavigation);
            Assert.IsNotNull(customer.ContactTypeIdentifierNavigation);
            Assert.IsNotNull(customer.CountryIdentfierNavigation);
        }

    }
}
