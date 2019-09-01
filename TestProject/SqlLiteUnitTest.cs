using System;
using System.Collections.Generic;
using System.Linq;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Classes;
using static Microsoft.EntityFrameworkCore.EF;

namespace TestProject
{
    [TestClass(), TestCategory("SqlLite")]
    public class SqlLiteUnitTest : TestBase
    {
        [TestInitialize]
        public void Init()
        {
  
        }
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
        /// <summary>
        /// Testing add range for customers with navigation of
        /// countries
        /// </summary>
        [TestMethod]
        public void AddRangeTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                context.Countries.AddRange(GetCountriesForSqlLite());
                var contactCount = context.SaveChanges();

                context.Customers.AddRange(MockedSqlLiteCustomers());
                var customerCount = context.SaveChanges();

                Assert.IsTrue(contactCount == 21,
                    "Expected 21 countries for SqlLite add range");
                Assert.IsTrue(customerCount == 4,
                    "Expected 4 customers for SqlLite add range");
            }

            /*
             * Optional, find by name
             */
            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                var horn = context.Customers
                    .FirstOrDefault(customer => customer.CompanyName == "Around the Horn");

                Assert.IsNotNull(horn);
            }
        }
        /// <summary>
        /// Test for updating a customer done in a disconnected scenario 
        /// </summary>
        [TestMethod]
        public void UpdateCustomerTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                context.Database.EnsureCreated();
            }

            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                context.Countries.AddRange(GetCountriesForSqlLite());
                context.SaveChanges();
                context.Customers.AddRange(MockedSqlLiteCustomers());
                context.SaveChanges();
            }

            Customer thisCustomer;
            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                thisCustomer = context.Customers
                    .FirstOrDefault(customer => customer.CompanyName == "Around the Horn");
            }

            // ReSharper disable once PossibleNullReferenceException
            thisCustomer.CompanyName = thisCustomer.CompanyName + "1";

            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                context.Attach(thisCustomer).State = EntityState.Modified;
                context.SaveChanges();
            }

            using (var context = new NorthWindContext(ContextSqlLiteOptions(connection)))
            {
                Assert.IsNotNull(context.Customers.FirstOrDefault(cust => cust.CompanyName == "Around the Horn1"),
                    "Expected to find Around the Horn1 in SqlLite update test");
            }
        }
    }
}
