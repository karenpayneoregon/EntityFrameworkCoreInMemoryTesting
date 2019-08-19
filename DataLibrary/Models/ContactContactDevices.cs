using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkCoreLikeLibrary.Models
{
    public partial class ContactContactDevices
    {
        [Key]
        public int Identifier { get; set; }
        public int? ContactIdentifier { get; set; }
        public int? PhoneTypeIdenitfier { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Active { get; set; }

        [ForeignKey("ContactIdentifier")]
        [InverseProperty("ContactContactDevices")]
        public virtual Contact ContactIdentifierNavigation { get; set; }
        [ForeignKey("PhoneTypeIdenitfier")]
        [InverseProperty("ContactContactDevices")]
        public virtual PhoneType PhoneTypeIdenitfierNavigation { get; set; }
    }
}
