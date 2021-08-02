using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BaseUnitTestProject
{
    [TestClass]
    public class CompiledQueries
    {
        [TestMethod]
        //[Ignore]
        public void CompiledQueryTest()
        {
            var ops = new DataLibraryCore.NorthWindOperations.Operations();
            var results1 = ops.GetCustomerById(2);
            Assert.IsTrue(results1.Name == "Ana Trujillo Emparedados y helados");

            var results2 = ops.GetCustomerById(15);
            Assert.IsTrue(results2.Name == "Drachenblut Delikatessen");
        }
        /// <summary>
        /// Data load should be larger with more iterations 
        /// </summary>
        [TestMethod]
        //[Ignore]
        public void LoadTest()
        {
            var ops = new DataLibraryCore.NorthWindOperations.Operations();
            for (int index = 0; index < 1000; index++)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                ops.GetCustomerById(15);
                stopwatch.Stop();

                Console.WriteLine($"{stopwatch.ElapsedMilliseconds,4}ms");
            }
        }
    }
}
