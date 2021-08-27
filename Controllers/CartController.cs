using MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSpecialProject.Models;


namespace WebSpecialProject.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private WebProjectEntities db = new WebProjectEntities();

        public ActionResult Index()
        {
            int x = Convert.ToInt32(Session["UserIDCTM"]);
            var productOnCarts = db.ProductOnCarts.Where(p => p.IdCart == x && p.Status == "InProcess");
            return View(productOnCarts.ToList());
        }
    
        public void Buy()
        {
            Response.Redirect("~/Order/CreateOrderCash");
        }
        public void BuyWithMomo()
        {
            Response.Redirect("~/Order/CreateOrderMoMo");
        }

    }

}