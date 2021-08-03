using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLibraryCore.Contexts;
using DataLibraryCore.Helpers;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibraryCore.NorthWindOperations
{
    public class Operations
    {
        private readonly NorthWindContext _context = new ();

        #region For use with large iterations e.g. 100,000 plus (list are not supported yet)

        private static readonly Func<NorthWindContext, int, CustomerSpecial> _getCustomerSpecialById =
            EF.CompileQuery((NorthWindContext context, int customerIdentifier) =>
                context.Customers.Select(
                    customer => new CustomerSpecial
                    {
                        Id = customer.CustomerIdentifier,
                        Name = customer.CompanyName,
                        Phone = customer.Phone,
                        CountryId = customer.CountryIdentfier
                    }).FirstOrDefault(customerSpecial => customerSpecial.Id == customerIdentifier));


        
        /// <summary>
        /// Get customer by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Found customer or null if not found</returns>
        public CustomerSpecial GetCustomerById(int id)
        {
            var temp = _context.Customers.ToList();
            var test = _getCustomerSpecialById(_context, id);

            return test;
        }

        #endregion


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
        /// <summary>
        /// Serialize list of <see cref="CustomerEntity"/> List of <see cref="ContactType"/> list of <see cref="Contact"/> and list of <see cref="Countries"/>
        /// to files under current project, JsonData folder.
        /// </summary>
        /// <returns>Empty <see cref="Task"/></returns>
        public static async Task SerializeModelsToJson()
        {

            await using var context = new NorthWindContext();
            List<CustomerEntity> cust = await AllCustomersToJsonAsync();
            await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Customers.json"), JsonHelpers.Serialize<CustomerEntity>(cust));

            List<ContactType> contactTypes = context.ContactType.ToList();
            await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "ContactType.json"), JsonHelpers.Serialize<ContactType>(contactTypes));

            List<Contact> contacts = context.Contact.ToList();
            await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Contacts.json"), JsonHelpers.Serialize<Contact>(contacts));

            List<Countries> countriesList = context.Countries.ToList();
            await File.WriteAllTextAsync(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JsonData", "Countries.json"), JsonHelpers.Serialize<Countries>(countriesList));

        }

    }
}
