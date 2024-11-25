using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;
using System.Threading;
using System;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("Test output");
            Person tom = new Person();
            tom.Print();

            int milliseconds = 2000;
            Thread.Sleep(milliseconds);
            Assert.IsTrue(false);
        }
    }
}
