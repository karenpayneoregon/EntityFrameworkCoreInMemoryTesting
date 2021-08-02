using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BaseUnitTestProject.Classes
{
    public class MockedData
    {
        public List<Contact> ContactList { get; set; }
        protected static List<Contact> MockedContacts()
        {
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contacts.json");
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(fileName));
            return contacts;

        }
        /// <summary>
        /// Setup in memory contacts.
        /// Note context.Contact.Clear(), without this had some
        /// strange things happen, will look deeper into this.
        /// </summary>
        /// <returns></returns>
        public static List<Contact> PrepareContacts()
        {

            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: "Add_Contacts_to_database")
                .Options;

            using var context = new NorthWindContext(options);
            context.Database.EnsureDeleted();
            context.Contact.AddRange(MockedContacts());
            context.SaveChanges();

            return context.Contact.ToList();
        }
        /// <summary>
        /// Mocked up customers for various in-memory test
        /// </summary>
        /// <returns></returns>
        public static List<Customer> MockedInMemoryCustomers()
        {
            var customers = new List<Customer>()
            {
                new () {CompanyName = "Ana Trujillo Emparedados y helados", ContactIdentifier = 2, ContactTypeIdentifier = 7,CountryIdentfier = 12},
                new () {CompanyName = "Antonio Moreno Taquería", ContactIdentifier = 3, ContactTypeIdentifier = 7,CountryIdentfier = 12},
                new () {CompanyName = "Around the Horn", ContactIdentifier = 4, ContactTypeIdentifier = 12, CountryIdentfier = 19},
                new () {CompanyName = "Berglunds snabbköp", ContactIdentifier = 5, ContactTypeIdentifier = 6, CountryIdentfier = 17},
                new () {CompanyName = "Blauer See Delikatessen", ContactIdentifier = 6, ContactTypeIdentifier = 12,CountryIdentfier = 9},
                new () {CompanyName = "Blondesddsl père et fils", ContactIdentifier = 7, ContactTypeIdentifier = 5,CountryIdentfier = 8},
                new () {CompanyName = "Bólido Comidas preparadia", ContactIdentifier = 8, ContactTypeIdentifier = 7,CountryIdentfier = 16},
                new () {CompanyName = "Cactus Comidas para llevar", ContactIdentifier = 11, ContactTypeIdentifier = 9,CountryIdentfier = 1},
                new () {CompanyName = "Consolidated Holdings", ContactIdentifier = 14, ContactTypeIdentifier = 12,CountryIdentfier = 19},
                new () {CompanyName = "Drachenblut Delikatessen", ContactIdentifier = 15, ContactTypeIdentifier = 6,CountryIdentfier = 9},
                new () {CompanyName = "Du monde entier", ContactIdentifier = 16, ContactTypeIdentifier = 7, CountryIdentfier = 8},
                new () {CompanyName = "Eastern Connection", ContactIdentifier = 17, ContactTypeIdentifier = 9, CountryIdentfier = 19},
                new () {CompanyName = "Ernst Handel", ContactIdentifier = 18, ContactTypeIdentifier = 11, CountryIdentfier = 2},
                new () {CompanyName = "FISSA Fabrica Inter. Salchichas S.A.", ContactIdentifier = 15, ContactTypeIdentifier = 1,CountryIdentfier = 16},
                new () {CompanyName = "Folies gourmandes", ContactIdentifier = 20, ContactTypeIdentifier = 2, CountryIdentfier = 8},
                new () {CompanyName = "Folk och fä HB", ContactIdentifier = 21, ContactTypeIdentifier = 7, CountryIdentfier = 17},
                new () {CompanyName = "Frankenversand", ContactIdentifier = 22, ContactTypeIdentifier = 5, CountryIdentfier = 9},
                new () {CompanyName = "France restauration", ContactIdentifier = 23, ContactTypeIdentifier = 5, CountryIdentfier = 8},
                new () {CompanyName = "Franchi S.p.A.", ContactIdentifier = 24, ContactTypeIdentifier = 12, CountryIdentfier = 11},
                new () {CompanyName = "Furia Bacalhau e Frutos do Mar", ContactIdentifier = 25, ContactTypeIdentifier = 11,CountryIdentfier = 15}
            };

            return customers;

        }
        protected List<Countries> GetCountriesForSqlLite()
        {
            var nameList = new List<string>() { "Argentina", "Austria", "Belgium", "Brazil", "Canada", "Denmark", "Finland", "France", "Germany", "Ireland", "Italy", "Mexico", "Norway", "Poland", "Portugal", "Spain", "Sweden", "Switzerland", "UK", "USA", "Venezuela" };
            var countries = nameList.Select(cn => new Countries() { CountryName = cn });
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
                new () {CompanyName = "Ana Trujillo Emparedados y helados", CountryIdentfier = 1},
                new () {CompanyName = "Antonio Moreno Taquería", CountryIdentfier = 9},
                new () {CompanyName = "Around the Horn", CountryIdentfier = 9},
                new () {CompanyName = "Berglunds snabbköp", CountryIdentfier = 8}
            };

            for (var index = 0; index < customers.Count; index++)
            {
                customers[index].ModifiedDate = DateTime.Now;
            }
            return customers;

        }

    }
}
