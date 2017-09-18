using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GitTeamCityDemo.Controllers
{
    public class HomeController : Controller
    {
        private HomeBusinessLayer _bdc { get; set; }

        public HomeController()
        {
            this._bdc = new HomeBusinessLayer();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string ForTestingFirst(int val)
        {
            return val.ToString();
        }

        public string ForTestingSecond(int val)
        {
            return val.ToString();
        }

        public string CalculateSum(int first, int second)
        {
            var retVal = this._bdc.CalculateSum(first, second);
            return retVal.ToString();
        }
    }
}