using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer;
using GitTeamCityDemo.Controllers;

namespace IntegrationTest
{
    [TestClass]
    public class UnitTest1
    {
        HomeController _controller;

        public UnitTest1()
        {
            this._controller = new HomeController();
        }

        [TestMethod]
        public void TestMethod1()
        {
            int first = 10, second = 20;
            string result = this._controller.CalculateSum(first, second);

            Assert.IsTrue( result.Equals("30") );
        }
    }
}
