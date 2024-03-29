﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using Newtonsoft.Json;

namespace TestProject.Classes
{
    
    /// <summary>
    /// Mock up data for test. Two different methods are used
    /// For your test use one or the other.
    /// </summary>
    public class MockedData
    {
        public List<Contact> ContactList { get; set; } 
        protected List<Contact> MockedContacts()
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contacts.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(fileName));
            return contacts;

        }

        /// <summary>
        /// Mocked up customers for various in-memory test
        /// </summary>
        /// <returns></returns>
        protected static List<Customer> MockedInMemoryCustomers()
        {
            var customers = new List<Customer>()
            {
                new Customer() {CompanyName = "Ana Trujillo Emparedados y helados", ContactIdentifier = 2, ContactTypeIdentifier = 7,CountryIdentfier = 12},
                new Customer() {CompanyName = "Antonio Moreno Taquería", ContactIdentifier = 3, ContactTypeIdentifier = 7,CountryIdentfier = 12},
                new Customer() {CompanyName = "Around the Horn", ContactIdentifier = 4, ContactTypeIdentifier = 12, CountryIdentfier = 19},
                new Customer() {CompanyName = "Berglunds snabbköp", ContactIdentifier = 5, ContactTypeIdentifier = 6, CountryIdentfier = 17},
                new Customer() {CompanyName = "Blauer See Delikatessen", ContactIdentifier = 6, ContactTypeIdentifier = 12,CountryIdentfier = 9},
                new Customer() {CompanyName = "Blondesddsl père et fils", ContactIdentifier = 7, ContactTypeIdentifier = 5,CountryIdentfier = 8},
                new Customer() {CompanyName = "Bólido Comidas preparadia", ContactIdentifier = 8, ContactTypeIdentifier = 7,CountryIdentfier = 16},
                new Customer() {CompanyName = "Cactus Comidas para llevar", ContactIdentifier = 11, ContactTypeIdentifier = 9,CountryIdentfier = 1},
                new Customer() {CompanyName = "Consolidated Holdings", ContactIdentifier = 14, ContactTypeIdentifier = 12,CountryIdentfier = 19},
                new Customer() {CompanyName = "Drachenblut Delikatessen", ContactIdentifier = 15, ContactTypeIdentifier = 6,CountryIdentfier = 9},
                new Customer() {CompanyName = "Du monde entier", ContactIdentifier = 16, ContactTypeIdentifier = 7, CountryIdentfier = 8},
                new Customer() {CompanyName = "Eastern Connection", ContactIdentifier = 17, ContactTypeIdentifier = 9, CountryIdentfier = 19},
                new Customer() {CompanyName = "Ernst Handel", ContactIdentifier = 18, ContactTypeIdentifier = 11, CountryIdentfier = 2},
                new Customer() {CompanyName = "FISSA Fabrica Inter. Salchichas S.A.", ContactIdentifier = 15, ContactTypeIdentifier = 1,CountryIdentfier = 16},
                new Customer() {CompanyName = "Folies gourmandes", ContactIdentifier = 20, ContactTypeIdentifier = 2, CountryIdentfier = 8},
                new Customer() {CompanyName = "Folk och fä HB", ContactIdentifier = 21, ContactTypeIdentifier = 7, CountryIdentfier = 17},
                new Customer() {CompanyName = "Frankenversand", ContactIdentifier = 22, ContactTypeIdentifier = 5, CountryIdentfier = 9},
                new Customer() {CompanyName = "France restauration", ContactIdentifier = 23, ContactTypeIdentifier = 5, CountryIdentfier = 8},
                new Customer() {CompanyName = "Franchi S.p.A.", ContactIdentifier = 24, ContactTypeIdentifier = 12, CountryIdentfier = 11},
                new Customer() {CompanyName = "Furia Bacalhau e Frutos do Mar", ContactIdentifier = 25, ContactTypeIdentifier = 11,CountryIdentfier = 15}
            };

            return customers;

        }

        protected List<Countries> GetCountriesForSqlLite()
        {
            var nameList = new List<string>() { "Argentina", "Austria", "Belgium", "Brazil", "Canada", "Denmark", "Finland", "France", "Germany", "Ireland", "Italy", "Mexico", "Norway", "Poland", "Portugal", "Spain", "Sweden", "Switzerland", "UK", "USA","Venezuela" };
            var countries = nameList.Select(cn => new Countries() {CountryName = cn});
            return countries.ToList();
        }

        /// <summary>
        /// Mocked up customers for various in-memory test
        /// </summary>
        /// <returns></returns>
        protected static List<Customer> MockedSqlLiteCustomers()
        {
            var customers = new List<Customer>()
            {
                new Customer() {CompanyName = "Ana Trujillo Emparedados y helados", CountryIdentfier = 1},
                new Customer() {CompanyName = "Antonio Moreno Taquería", CountryIdentfier = 9},
                new Customer() {CompanyName = "Around the Horn", CountryIdentfier = 9},
                new Customer() {CompanyName = "Berglunds snabbköp", CountryIdentfier = 8}
            };

            for (var index = 0; index < customers.Count; index++)
            {
                customers[index].ModifiedDate = DateTime.Now;
            }
            return customers;

        }
        
        
    }
}
