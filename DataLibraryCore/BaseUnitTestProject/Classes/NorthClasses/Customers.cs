using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BaseUnitTestProject.Classes.NorthClasses
{
    public partial class Customers : INotifyPropertyChanged //, IEquatable<Customers>
    {

        public Customers()
        {
            ContactTypeNavigation = new ContactType();
            Contact = new Contacts();
            CountryNavigation = new Countries();
        }
        
        private int? _countryIdentifier;
        private string _countryName;
        public int Id => CustomerIdentifier;
        public int CustomerIdentifier { get; set; }
        public string CompanyName { get; set; }

        public int? ContactId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }

        public string PostalCode { get; set; }

        /// <summary>
        /// CountryIdentifier
        /// </summary>
        public int CountryIdentifier
        {
            get => (int)_countryIdentifier;
            set
            {
                _countryIdentifier = value;
                OnPropertyChanged();
            }
        }

        public string CountryName
        {
            get => _countryName;
            set
            {
                _countryName = value;
                OnPropertyChanged();
            }
        }

        public int? ContactTypeIdentifier { get; set; }
        public DateTime? LastUpdated { get; set; }

        public Contacts Contact { get; set; }

        public  ContactType ContactTypeNavigation { get; set; }
        
        public Countries CountryNavigation { get; set; }

        public int ContactIdentifier { get; set; }

        public override string ToString() => CompanyName;
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public bool Equals(Customers other)
        //{
        //    if (ReferenceEquals(null, other)) return false;
        //    if (ReferenceEquals(this, other)) return true;
        //    return CompanyName == other.CompanyName;
        //}

        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj)) return false;
        //    if (ReferenceEquals(this, obj)) return true;
        //    if (obj.GetType() != this.GetType()) return false;
        //    return Equals((Customers) obj);
        //}

        //public override int GetHashCode()
        //{
        //    return (CompanyName != null ? CompanyName.GetHashCode() : 0);
        //}


    }

}
