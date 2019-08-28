using System.Collections.Generic;
using DataLibrary.Models;

namespace DataLibrary.Interfaces
{
    public interface ICustomer
    {
        /// <summary>
        /// Get Customer by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer GetById(int id);
        /// <summary>
        /// Update customer
        /// </summary>
        /// <param name="pCustomer"></param>
        /// <returns></returns>
        Customer Update(Customer pCustomer);
        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="pCustomer"></param>
        /// <returns></returns>
        Customer Add(Customer pCustomer);
        /// <summary>
        /// Remove customer by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customer Delete(int id);
        int Commit();
        List<Customer> GetAll();
    }
}
