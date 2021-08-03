namespace BaseUnitTestProject.Classes.NorthClasses
{
    public partial class Countries
    {

        public int Id => CountryIdentifier;
        public int CountryIdentifier { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;


    }
}