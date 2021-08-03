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
        /// Single instance of the <see cref="NorthWindContext"/> for in-memory context
        /// </summary>
        private NorthWindContext Context;

        /// <summary>
        /// Read contact data from json file into List&lt;Contact&gt;
        /// </summary>
        /// <returns></returns>
        protected List<Contact> MockedContacts()
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contacts.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(fileName));
            return contacts;

        }

        
        /// <summary>
        /// Code to run before each test method
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            
            _contactList = MockedData.PrepareContacts();
            
            Context = new NorthWindContext(dbContextOptions);
            
            if (TestContext.TestName != nameof(CustomersUpdateTest) && TestContext.TestName != nameof(CreateCustomer))
            {
                Context.Customers.AddRange(MockedData.MockedInMemoryCustomers());
                Context.SaveChanges();
            }

            if (TestContext.TestName == nameof(LoadingRelations))
            {
                LoadJoinedData();
            }

        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }

        #region Json files
        private static readonly string customersJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Customers.json");
        private static readonly string contactTypeJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "ContactType.json");
        private static readonly string contactsJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Contacts.json");
        private static readonly string countriesJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Countries.json");
        #endregion


        protected Customer MockSingleCustomer()
        {
            var customersList = CustomersContactCountries(out var contactTypeList, out var contactList, out var countriesList);

            var customer = customersList.FirstOrDefault();
            var contact = contactList.FirstOrDefault();
            var country = countriesList.Skip(2).FirstOrDefault();
            var contactType = contactTypeList.Skip(3).FirstOrDefault();


            Customer singleCustomer = new Customer();

            singleCustomer.CountryIdentfier = 8;
            singleCustomer.CompanyName = "Karen's Coffee shop";
            singleCustomer.Street = customer.Street;
            singleCustomer.City = customer.City;
            singleCustomer.PostalCode = customer.PostalCode;
            singleCustomer.CountryIdentfierNavigation = country;
            singleCustomer.CountryIdentfier = country.Id;

            singleCustomer.ContactIdentifierNavigation = contact;
            singleCustomer.FirstName = contact.FirstName;
            singleCustomer.LastName = contact.LastName;
            singleCustomer.ContactIdentifier = contact.ContactIdentifier;
            singleCustomer.ContactTypeIdentifierNavigation = contactType;
            singleCustomer.ContactTypeIdentifier = contactType.ContactTypeIdentifier;
            
            singleCustomer.InUse = true;

            /*
             * in-memory does not support HasDefaultValueSql
             */
            singleCustomer.ModifiedDate = DateTime.Now;
            

            Context.Customers.Add(singleCustomer);
            Context.SaveChanges();

            return singleCustomer;

        }
        public void LoadJoinedData()
        {
            Context = new NorthWindContext(dbContextOptions);
            
            var customersList = CustomersContactCountries(out var contactTypeList, out var contactList, out var countriesList);

            Context.Customers.AddRange(customersList!);
            Context.ContactType.AddRange(contactTypeList!);
            Context.Contact.AddRange(contactList!);
            Context.Countries.AddRange(countriesList!);
            
            var count = Context.SaveChanges();

        }

        private static List<Customer> CustomersContactCountries(out List<ContactType> contactTypeList, out List<Contact> contactList, out List<Countries> countriesList)
        {
            List<Customer> customersList = JsonConvert.DeserializeObject<List<Customer>>(File.ReadAllText(customersJsonFileName));
            contactTypeList = JsonConvert.DeserializeObject<List<ContactType>>(File.ReadAllText(contactTypeJsonFileName));
            contactList = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(contactsJsonFileName));
            countriesList = JsonConvert.DeserializeObject<List<Countries>>(File.ReadAllText(countriesJsonFileName));
            return customersList;
        }
    }

}
