﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataLibrary.Models;
using EntityFrameworkCoreLikeLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Classes;
using static Microsoft.EntityFrameworkCore.EF;

namespace TestProject
{
    [TestClass(), TestCategory("SQL-Server provider InMemory")]
    public class UnitTest1 : TestBase
    {
        [TestInitialize]
        public void Init()
        {
            Console.WriteLine(TestContext.TestName);
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }
        /// <summary>
        /// Add new customer to InMemory container
        /// </summary>
        [TestMethod]
        public void AddCustomerTest() 
        {
            var options = new DbContextOptionsBuilder<NorthWindContext>()
                .UseInMemoryDatabase(databaseName: "Add_Customer_to_database")
                .Options;

            using (var context = new NorthWindContext(options)) 
            {
                var contact = new Contact()
                {
                    FirstName = "Karen",
                    LastName = "Payne"
                };
                context.Entry(contact).State = EntityState.Added;

                var customer = new Customers()
                {
                    CompanyName = "Karen's coffee shop",
                    ContactIdentifierNavigation = contact,
                    CountryIdentfier = 20
                };
                context.Entry(customer).State = EntityState.Added;

                var saveChangesCount = context.SaveChanges();

                Assert.IsTrue(saveChangesCount == 2, 
                    "Expect one customer and one contact to be added.");

                Console.WriteLine(customer.ContactIdentifier);
            }
        }
        [TestMethod]
        public void CustomersAddRangTest()
        {
            var customers = CustomersList();

            Assert.IsTrue(customers.Count == 20, 
                "Expected to have 20 customer");

            Assert.IsTrue(customers.Any(customer => customer.CustomerIdentifier != 0),
                "Expected all new customers to have a primary key");
        }
        /// <summary>
        /// Working with Like condition starts-with
        /// </summary>
        [TestMethod]
        public void CustomersLikeConditionStartsWithTest()
        {
            var customers = CustomersList();

            var startsWithToken = "Fr%";

            var startsWithResults = customers
                .Where(customer => Functions.Like(
                    customer.CompanyName, 
                    startsWithToken))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 3, 
                "Expected three customers for Like starts with");

        }
        /// <summary>
        /// Working with Like condition ends-with
        /// </summary>
        [TestMethod]
        public void CustomersLikeConditionEndsWithTest()
        {
            var customers = CustomersList();

            var endsWithToken = "%_a";

            var endsWithResults = customers
                .Where(customer => Functions.Like(
                    customer.CompanyName,
                    endsWithToken))
                .ToList();

            Assert.IsTrue(endsWithResults.Count == 2,
                "Expected 2 customers for Like ends with");

        }

        /// <summary>
        /// Working with Like condition ends-with
        /// </summary>
        [TestMethod]
        public void CustomersLikeConditionContainsTest() 
        {
            var customers = CustomersList();

            var containsWithToken = "%en%";

            var startsWithResults = customers
                .Where(customer => Functions.Like(
                    customer.CompanyName,
                    containsWithToken))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 5,
                "Expected 5 customers for Like starts with");

        }

        [TestMethod]
        public void ContactsAddRangTest()
        {
            var contacts = ContactList();
            Assert.IsTrue(contacts.Any(contact => contact.ContactIdentifier != 0),
                "Expected all new contacts to have a primary key");
        }
        [TestMethod]
        public void ContactsLastNameStartsWithTest()
        {
            var contacts = ContactList();
            var startsWithToken = "Cr%";

            var startsWithResults = contacts
                .Where(contact => Functions.Like(
                    contact.LastName,
                    startsWithToken))
                .ToList();

            Assert.IsTrue(startsWithResults.Count == 4,
                "Expected 4 customers for Like starts with");
        }

    }
}
