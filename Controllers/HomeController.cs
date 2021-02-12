using Management.Customfilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Management.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthenticationFilter("Admin", "staff", "User", "Developar")]
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthenticationFilter("Admin", "staff", "User", "Developar")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [CustomAuthenticationFilter("Admin", "staff", "User", "Developar")]

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}