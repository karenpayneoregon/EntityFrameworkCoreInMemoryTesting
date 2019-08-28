using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CompiledQueryTest()
        {
            var ops = new DataLibrary.NorthWindOperations.Operations();
            var results1 = ops.GetCompiledById(2);
            Assert.IsTrue(results1.Name == "Ana Trujillo Emparedados y helados");

            var results2 = ops.GetCompiledById(15);
            Assert.IsTrue(results2.Name == "Drachenblut Delikatessen");
        }
    }
}
