using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibraryCore.Models
{
    public partial class PhoneType
    {
        public PhoneType()
        {
            ContactContactDevices = new HashSet<ContactContactDevices>();
        }

        [Key]
        public int PhoneTypeIdenitfier { get; set; }
        public string PhoneTypeDescription { get; set; }

        [InverseProperty("PhoneTypeIdenitfierNavigation")]
        public virtual ICollection<ContactContactDevices> ContactContactDevices { get; set; }

        public override string ToString() => PhoneTypeDescription;
    }
}
