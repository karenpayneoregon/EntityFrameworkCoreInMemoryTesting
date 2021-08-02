using System;
using System.Linq;
using BaseUnitTestProject.Base;
using DataLibraryCore.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.EntityFrameworkCore.EF;

namespace BaseUnitTestProject
{
    [TestClass]
    public partial class MainTest : TestBase
    {
        [TestMethod]
        public void CustomersAddRangeTest()
        {
            var customers = CustomersInMemoryList();

            Assert.IsTrue(customers.Count == 20, "Expected to have 20 customer");

            Assert.IsTrue(customers.Any(customer => customer.CustomerIdentifier != 0),
                "Expected all new customers to have a primary key");
        }


    }
}
