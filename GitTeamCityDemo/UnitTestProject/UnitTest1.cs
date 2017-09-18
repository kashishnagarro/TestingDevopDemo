using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        HomeBusinessLayer _bdc;

        public UnitTest1()
        {
            this._bdc = new HomeBusinessLayer();
        }

        [TestMethod]
        public void TestMethod1()
        {
            int first = 10, second = 20;
            int result = this._bdc.CalculateSum(first, second);

            Assert.IsTrue(result == 30);
        }
    }
}
