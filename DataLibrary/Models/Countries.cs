using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLibrary.Models;

namespace EntityFrameworkCoreLikeLibrary.Models
{
    public partial class Countries
    {
        public Countries()
        {
            Customers = new HashSet<Customers>();
        }

        [Column("id")]
        public int Id { get; set; }
        public string CountryName { get; set; }

        [InverseProperty("CountryIdentfierNavigation")]
        public virtual ICollection<Customers> Customers { get; set; }
    }
}
