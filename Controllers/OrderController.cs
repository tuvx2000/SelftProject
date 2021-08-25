using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSpecialProject.Models;


namespace WebSpecialProject.Controllers
{
    public class OrderController : Controller
    {
        private WebProjectEntities db = new WebProjectEntities();

        // GET: Order
        public ActionResult Index()
        {
            int x = Convert.ToInt32(Session["UserIDCTM"]);
            var AgentProductsPerOrder = db.ProductOnCarts.GroupBy(test => test.IdOrder)
                   .Select(grp => grp.FirstOrDefault())
                   .Where(p => p.IdCart == x)
                   .ToList();
            List<Order> Results = new List<Order>();
            foreach(Order order in db.Orders.ToList())
            {
                foreach (var productx in AgentProductsPerOrder)
                {
                    if(productx.IdOrder == order.ID)
                    {
                        Results.Add(order);
                    }
                }
            }
            return View(Results);
        }
        public void CreateOrder()
        {
            int x = Convert.ToInt32(Session["UserIDCTM"]);

            Order order = new Order();//Tao Order
            order.Status = "Delivering";
            order.TimeBought = DateTime.Now.ToString();
            order.Adress = db.Users.Find(x).Address;
            Random rand = new Random();
            order.ID = rand.Next(0, 100000000);
            db.Orders.Add(order);

            double? cost = 0;// Update Table OnSale
            var productOnCarts = db.ProductOnCarts.Where(p => p.IdCart == x);
            foreach (ProductOnCart product in productOnCarts)
            {
                if (product.Status.Equals("InProcess "))
                { 
                    product.Status = "Payed";
                    product.IdOrder = order.ID;
                    ProductToSell productToSell = db.ProductToSells.Find(product.IdProduct);
                    cost += productToSell.Price * product.QuantityOnCart;
                }
            }
            db.Carts.Find(x).TotalCost = cost;
            db.Orders.Find(order.ID).TotalCost = cost;
            Session["TotalCostOnCart"] = cost;



            db.SaveChanges();
            Session["AmmountOnCart"] = db.ProductOnCarts.Where(p => p.Status == "InProcess").Count();
            Response.Redirect("~/Home/Index");
        }
        [HttpPost]
        public ActionResult DetailOrder(FormCollection form)
        {
            int orderId = Int32.Parse(form["IdOrder"]);

            @Session["check-text"] = orderId;//form["IdOrder"];
            List<ProductOnCart> listProductOnDetail = db.ProductOnCarts.Where(p => p.IdOrder == orderId).ToList();
            ViewBag.TotalCost = db.Orders.Find(orderId).TotalCost;

            return View(listProductOnDetail);
        }
        
         [HttpPost]
        public void ConfirmReceived(FormCollection form)
        {
            int orderId = Int32.Parse(form["IdOrder"]);

            @Session["check-text"] = orderId;//form["IdOrder"];
            db.Orders.Where(p => p.ID == orderId).ToList().FirstOrDefault().Status = "Done";
            db.SaveChanges();

            Response.Redirect("~/Order/Index");
        }
    }
}