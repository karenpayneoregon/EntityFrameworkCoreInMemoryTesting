using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseUnitTestProject.Base;
using BaseUnitTestProject.Classes;
using DataLibraryCore.Contexts;
using DataLibraryCore.Models;
using Microsoft.EntityFrameworkCore;


// ReSharper disable once CheckNamespace - do not change
namespace BaseUnitTestProject
{
    public partial class MainTest
    {
        private List<Contact> _contactList;

        [TestInitialize]
        public void Init()
        {

            //if (TestContext.TestName == nameof(ContactsLastNameStartsWithTest))
            //{
            //    _contactList = MockedData.PrepareContacts();
            //}
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            TestResults = new List<TestContext>();
        }



    }

}
