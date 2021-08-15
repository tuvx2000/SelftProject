using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSpecialProject.Models;

namespace WebSpecialProject.Controllers
{
    public class HomeController : Controller
    {
        WebProjectEntities dbcontext = new WebProjectEntities();

        public ActionResult Index()
        {
            var list = dbcontext.Foods.ToList();
            var x = list[0];
            var y = x.FoodName;
            return View(list);
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
    }
}