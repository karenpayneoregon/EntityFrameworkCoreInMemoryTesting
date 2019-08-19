using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLibrary.Models;

namespace EntityFrameworkCoreLikeLibrary.Models
{
    public partial class Contact
    {
        public Contact()
        {
            ContactContactDevices = new HashSet<ContactContactDevices>();
            Customers = new HashSet<Customers>();
        }

        [Key]
        public int ContactIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public bool? InUse { get; set; }

        [InverseProperty("ContactIdentifierNavigation")]
        public virtual ICollection<ContactContactDevices> ContactContactDevices { get; set; }
        [InverseProperty("ContactIdentifierNavigation")]
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
