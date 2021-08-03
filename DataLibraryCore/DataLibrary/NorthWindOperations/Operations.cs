using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLibraryCore.Contexts;
using DataLibraryCore.Helpers;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DataLibraryCore.NorthWindOperations
{
    public class Operations
    {
        private readonly NorthWindContext _context = new ();

        #region For use with large iterations e.g. 100,000 plus (list are not supported yet)
        /// <summary>
        /// From live data
        /// Given a <see cref="NorthWindContext"/> return a <see cref="CustomerSpecial"/>
        /// </summary>
        private static readonly Func<NorthWindContext, int, CustomerSpecial> _getCustomerSpecialById =
            // pre-compile Select statement
            EF.CompileQuery((NorthWindContext context, int customerIdentifier) =>
                // get data
                context.Customers.Select(
                    customer => new CustomerSpecial
                    {
                        Id = customer.CustomerIdentifier,
                        Name = customer.CompanyName,
                        Phone = customer.Phone,
                        CountryId = customer.CountryIdentfier
                    })
            .FirstOrDefault(customerSpecial => customerSpecial.Id == customerIdentifier));


        
        /// <summary>
        /// Get customer from database by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found customer or null if not found</returns>
        public CustomerSpecial GetCustomerById(int id) => _getCustomerSpecialById(_context, id);

        #endregion

        /// <summary>
        /// Read all customers from database with
        ///     Contacts
        ///     Into <see cref="Customer.Projection"/>
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CustomerEntity>> AllCustomersToJsonAsync()
        {

            return await Task.Run(async () =>
            {
                await using var context = new NorthWindContext();
                List<CustomerEntity> customerItemsList = await context.Customers
                    .Include(customer => customer.ContactIdentifierNavigation)
                    .Select(Customer.Projection)
                    .ToListAsync();

                return customerItemsList.OrderBy((customer) => customer.CompanyName).ToList();
            });

        }



        #region Json serialization for unit test (for a real app this should not be here)

        private static readonly string customersJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Customers.json");
        private static readonly string contactTypeJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "ContactType.json");
        private static readonly string contactsJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Contacts.json");
        private static readonly string countriesJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", "Countries.json");

        /// <summary>
        /// Serialize list of <see cref="CustomerEntity"/> List of <see cref="ContactType"/> list of <see cref="Contact"/> and list of <see cref="Countries"/>
        /// to files under current project, JsonData folder.
        /// </summary>
        /// <returns>Empty <see cref="Task"/></returns>
        public static async Task SerializeModelsToJson()
        {

            await using var context = new NorthWindContext();
            List<CustomerEntity> cust = await AllCustomersToJsonAsync();
            await File.WriteAllTextAsync(customersJsonFileName, JsonHelpers.Serialize<CustomerEntity>(cust));

            List<ContactType> contactTypes = context.ContactType.ToList();
            await File.WriteAllTextAsync(contactTypeJsonFileName, JsonHelpers.Serialize<ContactType>(contactTypes));

            List<Contact> contacts = context.Contact.ToList();
            await File.WriteAllTextAsync(contactsJsonFileName, JsonHelpers.Serialize<Contact>(contacts));

            List<Countries> countriesList = context.Countries.ToList();
            await File.WriteAllTextAsync(countriesJsonFileName, JsonHelpers.Serialize<Countries>(countriesList));

        }

        public static List<CustomerEntity> ReadCustomersWithJoins()
        {
            var customerJson = File.ReadAllText(customersJsonFileName);
            var customersList = JsonConvert.DeserializeObject<List<Customer>>(customerJson);

            var contactTypeJson = File.ReadAllText(contactTypeJsonFileName);
            var contactTypeList = JsonConvert.DeserializeObject<List<ContactType>>(contactTypeJson);

            var contactJson = File.ReadAllText(contactsJsonFileName);
            var contactList = JsonConvert.DeserializeObject<List<Contact>>(contactJson);

            var countriesJson = File.ReadAllText(countriesJsonFileName);
            var countriesList = JsonConvert.DeserializeObject<List<Countries>>(countriesJson);


            List<CustomerEntity> customerData = (

                from customer in customersList

                join contact in contactList on customer.ContactIdentifier equals contact.ContactIdentifier
                join contactType in contactTypeList on customer.ContactTypeIdentifier equals contactType.ContactTypeIdentifier
                join country in countriesList on customer.CountryIdentfier equals country.Id

                select new CustomerEntity
                {
                    CustomerIdentifier = customer.CustomerIdentifier,
                    CompanyName = customer.CompanyName,
                    ContactIdentifier = customer.ContactIdentifier,
                    ContactTitle = contactType.ContactTitle,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Address = customer.Street,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    CountryIdentifier = customer.CountryIdentfier,
                    CountyName = customer.CountryIdentfierNavigation.CountryName,
                    Contacts = contact

                }).ToList();

            return customerData;

        }

        #endregion

    }
}
