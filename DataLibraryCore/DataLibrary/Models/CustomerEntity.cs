using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace DataLibraryCore.Models
{
    public class CustomerEntity : INotifyPropertyChanged
    {
        #region fields

        private string _companyName;
        private object _contactIdentifier;
        private string _firstName;
        private string _lastName;
        private int _contactTypeIdentifier;
        private string _contactTitle;
        private object _address;
        private string _city;
        private string _postalCode;
        private object _countryIdentifier;
        private object _countyName;

        #endregion

        [JsonIgnore]
        public int CustomerIdentifier { get; set; }

        public string CompanyName
        {
            get => _companyName;
            set
            {
                _companyName = value;
                OnPropertyChanged();
            }
        }

        public object ContactIdentifier
        {
            get => _contactIdentifier;
            set
            {
                _contactIdentifier = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public int ContactTypeIdentifier
        {
            get => _contactTypeIdentifier;
            set
            {
                _contactTypeIdentifier = value;
                OnPropertyChanged();
            }
        }

        public string ContactTitle
        {
            get => _contactTitle;
            set
            {
                _contactTitle = value;
                OnPropertyChanged();
            }
        }

        public Contact Contacts { get; set; }

        public object Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                _city = value;
                OnPropertyChanged();
            }
        }

        public string PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                OnPropertyChanged();
            }
        }

        public object CountryIdentifier
        {
            get => _countryIdentifier;
            set
            {
                _countryIdentifier = value;
                OnPropertyChanged();
            }
        }

        public object CountyName
        {
            get => _countyName;
            set
            {
                _countyName = value;
                OnPropertyChanged();
            }
        }

        public Countries CountryNavigation { get; set; }
        public virtual Contact ContactIdentifierNavigation { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}