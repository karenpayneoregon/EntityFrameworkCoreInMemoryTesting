using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLibraryCore.Models
{
    [Table("Customers")]
    public partial class Customer
    {
        public int CustomerIdentifier { get; set; }
        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; }
        public int? ContactIdentifier { get; set; }
        public int? ContactTypeIdentifier { get; set; }
        [StringLength(60)]
        public string Street { get; set; }
        [StringLength(15)]
        public string City { get; set; }
        [StringLength(10)]
        public string PostalCode { get; set; }
        public int? CountryIdentfier { get; set; }
        [StringLength(24)]
        public string Phone { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public bool? InUse { get; set; }

        [ForeignKey("ContactIdentifier")]
        [InverseProperty("Customers")]
        public virtual Contact ContactIdentifierNavigation { get; set; }
        [ForeignKey("ContactTypeIdentifier")]
        [InverseProperty("Customers")]
        public virtual ContactType ContactTypeIdentifierNavigation { get; set; }
        [ForeignKey("CountryIdentfier")]
        [InverseProperty("Customers")]
        public virtual Countries CountryIdentfierNavigation { get; set; }

        public override string ToString() => $"{CustomerIdentifier} {CompanyName}";
    }
}