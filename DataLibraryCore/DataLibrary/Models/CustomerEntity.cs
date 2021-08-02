namespace DataLibraryCore.Models
{
    public class CustomerEntity
    {
        public int CustomerIdentifier { get; set; }
        public string CompanyName { get; set; }
        public object ContactIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactTypeIdentifier { get; set; }
        public string ContactTitle { get; set; }
        public object Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public object CountryIdentifier { get; set; }
        public object CountyName { get; set; }
    }
}