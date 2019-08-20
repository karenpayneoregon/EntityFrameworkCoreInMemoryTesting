namespace DataLibrary.Models
{
    public interface ICustomer
    {
        Customer GetById(int id);
        Customer Update(Customer pCustomers);
        Customer Add(Customer pCustomers);
        Customer Delete(int id);
        int Commit();
    }
}
