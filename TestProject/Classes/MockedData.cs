using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;

namespace TestProject.Classes
{
    public class MockedData
    {
        protected List<Contact> ReadContacts()
        {
            var contacts = new List<Contact>();
            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "contacts.txt");
            var fileContents = File.ReadAllLines(fileName);

            foreach (var line in fileContents)
            {
                var fields = line.Split(',');
                contacts.Add(new Contact()
                {
                    FirstName = fields[1],
                    LastName = fields[2],
                    ModifiedDate = Convert.ToDateTime(fields[3]),
                    InUse = Convert.ToInt32(fields[4]) != 0
                });
            }

            return contacts;
        }

        /// <summary>
        /// Mocked up customers for various test
        /// </summary>
        /// <returns></returns>
        protected static List<Customers> MockedCustomers()
        {
            var customers = new List<Customers>()
            {
                new Customers() {CompanyName = "Ana Trujillo Emparedados y helados", ContactIdentifier = 2, ContactTypeIdentifier = 7,CountryIdentfier = 12},
                new Customers() {CompanyName = "Antonio Moreno Taquería", ContactIdentifier = 3, ContactTypeIdentifier = 7,CountryIdentfier = 12},
                new Customers() {CompanyName = "Around the Horn", ContactIdentifier = 4, ContactTypeIdentifier = 12, CountryIdentfier = 19},
                new Customers() {CompanyName = "Berglunds snabbköp", ContactIdentifier = 5, ContactTypeIdentifier = 6, CountryIdentfier = 17},
                new Customers() {CompanyName = "Blauer See Delikatessen", ContactIdentifier = 6, ContactTypeIdentifier = 12,CountryIdentfier = 9},
                new Customers() {CompanyName = "Blondesddsl père et fils", ContactIdentifier = 7, ContactTypeIdentifier = 5,CountryIdentfier = 8},
                new Customers() {CompanyName = "Bólido Comidas preparadia", ContactIdentifier = 8, ContactTypeIdentifier = 7,CountryIdentfier = 16},
                new Customers() {CompanyName = "Cactus Comidas para llevar", ContactIdentifier = 11, ContactTypeIdentifier = 9,CountryIdentfier = 1},
                new Customers() {CompanyName = "Consolidated Holdings", ContactIdentifier = 14, ContactTypeIdentifier = 12,CountryIdentfier = 19},
                new Customers() {CompanyName = "Drachenblut Delikatessen", ContactIdentifier = 15, ContactTypeIdentifier = 6,CountryIdentfier = 9},
                new Customers() {CompanyName = "Du monde entier", ContactIdentifier = 16, ContactTypeIdentifier = 7, CountryIdentfier = 8},
                new Customers() {CompanyName = "Eastern Connection", ContactIdentifier = 17, ContactTypeIdentifier = 9, CountryIdentfier = 19},
                new Customers() {CompanyName = "Ernst Handel", ContactIdentifier = 18, ContactTypeIdentifier = 11, CountryIdentfier = 2},
                new Customers() {CompanyName = "FISSA Fabrica Inter. Salchichas S.A.", ContactIdentifier = 15, ContactTypeIdentifier = 1,CountryIdentfier = 16},
                new Customers() {CompanyName = "Folies gourmandes", ContactIdentifier = 20, ContactTypeIdentifier = 2, CountryIdentfier = 8},
                new Customers() {CompanyName = "Folk och fä HB", ContactIdentifier = 21, ContactTypeIdentifier = 7, CountryIdentfier = 17},
                new Customers() {CompanyName = "Frankenversand", ContactIdentifier = 22, ContactTypeIdentifier = 5, CountryIdentfier = 9},
                new Customers() {CompanyName = "France restauration", ContactIdentifier = 23, ContactTypeIdentifier = 5, CountryIdentfier = 8},
                new Customers() {CompanyName = "Franchi S.p.A.", ContactIdentifier = 24, ContactTypeIdentifier = 12, CountryIdentfier = 11},
                new Customers() {CompanyName = "Furia Bacalhau e Frutos do Mar", ContactIdentifier = 25, ContactTypeIdentifier = 11,CountryIdentfier = 15}
            };

            return customers;

        }
    }
}
