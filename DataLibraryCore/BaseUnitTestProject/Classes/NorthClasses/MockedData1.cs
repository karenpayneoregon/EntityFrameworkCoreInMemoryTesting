using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BaseUnitTestProject.Classes.NorthClasses;

using Newtonsoft.Json;

namespace BaseUnitTestProject.Classes.NorthClasses
{
    public class MockedData1
    {
        #region Json file paths

        private static readonly string customersJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Customers.json");
        private static readonly string contactTypeJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "ContactType.json");
        private static readonly string contactsJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Contacts.json");
        private static readonly string countriesJsonFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Countries.json");

        #endregion
        public static List<CustomerEntity> ReadCustomersWithJoins()
        {
            var customersList = JsonConvert.DeserializeObject<List<Customers>>(File.ReadAllText(customersJsonFileName));
            var contactTypeList = JsonConvert.DeserializeObject<List<ContactType>>(File.ReadAllText(contactTypeJsonFileName));
            var contactList = JsonConvert.DeserializeObject<List<Contacts>>(File.ReadAllText(contactsJsonFileName));
            var countriesList = JsonConvert.DeserializeObject<List<Countries>>(File.ReadAllText(countriesJsonFileName));


            List<CustomerEntity> customerData = (

                from customer in customersList

                join contact in contactList on customer.ContactId equals contact.ContactId
                join contactType in contactTypeList on customer.ContactTypeIdentifier equals contactType.ContactTypeIdentifier
                join country in countriesList on customer.CountryIdentifier equals country.CountryIdentifier

                select new CustomerEntity
                {
                    CustomerIdentifier = customer.CustomerIdentifier,
                    CompanyName = customer.CompanyName,
                    ContactIdentifier = customer.ContactId,
                    ContactTitle = contactType.ContactTitle,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Address = customer.Street,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    CountryIdentifier = customer.CountryIdentifier,
                    CountyName = customer.CountryName,
                    ContactTypeNavigation = contactType,
                    Contact = contact
                }).ToList();



            return customerData;

        }
    }
}
