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
            var productOnCarts = db.ProductOnCarts.Where(p => p.IdCart == x);
            return View(productOnCarts.ToList());
        }
    
        public ActionResult Buy()
        {
            double? cost = 0;
            int x = Convert.ToInt32(Session["UserIDCTM"]);
            var productOnCarts = db.ProductOnCarts.Where(p => p.IdCart == x);
            foreach( ProductOnCart product in productOnCarts){
                product.Status = "Done";
                ProductToSell productToSell = db.ProductToSells.Find(product.IdProduct);
                cost += productToSell.Price* product.QuantityOnCart;
            }
            db.Carts.Find(x).TotalCost = cost;
            Session["TotalCostOnCart"] = cost;
            db.SaveChanges();
            Response.Redirect("~/Cart/Index");
            return View();
        }
    }
}