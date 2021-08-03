using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataLibraryCore.Models
{
    public partial class Customer
    {
        [NotMapped] [JsonIgnore] public string FirstName { get; set; }
        [NotMapped] [JsonIgnore] public string LastName { get; set; }

        public static Expression<Func<Customer, CustomerEntity>> Projection
        {

            get
            {
                return (customer) => new CustomerEntity()
                {
                    CustomerIdentifier = customer.CustomerIdentifier,
                    CompanyName = customer.CompanyName,
                    Address = customer.Street,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    ContactTypeIdentifier = customer.ContactTypeIdentifier.Value,
                    ContactTitle = customer.ContactTypeIdentifierNavigation.ContactTitle,
                    CountyName = customer.CountryIdentfierNavigation.CountryName,
                    FirstName = customer.ContactIdentifierNavigation.FirstName,
                    LastName = customer.ContactIdentifierNavigation.LastName,
                    ContactIdentifier = Convert.ToInt32(customer.ContactIdentifier),
                    Contacts = customer.ContactIdentifierNavigation,
                    CountryIdentifier = customer.CountryIdentfier,
                    CountryNavigation = customer.CountryIdentfierNavigation
                };
            }

        }
    }
}
