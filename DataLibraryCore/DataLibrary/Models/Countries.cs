using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibraryCore.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Customers = new HashSet<Customer>();
        }

        [Column("id")]
        public int Id { get; set; }
        public string CountryName { get; set; }

        [InverseProperty("CountryIdentfierNavigation")]
        public virtual ICollection<Customer> Customers { get; set; }

        public override string ToString() => CountryName;
    }
}
