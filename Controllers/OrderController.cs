using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSpecialProject.Models;
using MoMo;
using Newtonsoft.Json.Linq;

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
        public void CreateOrderMoMo()
        {
            int orderId = CreateOrder("MoMo");
            BuyWithMomoStepToServer(orderId);

        }
        public void CreateOrderCash()
        {
            CreateOrder("Cash");
            Response.Redirect("~/Home/Index");

        }

        public  int CreateOrder(string chanelPay)
        {
            int x = Convert.ToInt32(Session["UserIDCTM"]);

            Order order = new Order();//Tao Order
            if(chanelPay == "Cash")
               order.Status = "Cash-Payed";
            else if(chanelPay == "MoMo")
                order.Status = "MoMo-InProcess";


            order.TimeBought = DateTime.Now.ToString();
            order.Adress = db.Users.Find(x).Address;
            Random rand = new Random();
            order.IdOnMomo = rand.Next(100000, 100000000);
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

            return order.ID;
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






        public void BuyWithMomoStepToServer(int orderIdMoMo)
        {
            string endpoint = "https://payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMODKHI20210823";
            string accessKey = "1G3T2L88zJ2zyeSs";
            string secretKey = "nDKAK4Sx6v4o9vJ9lKuZ9qHmwnePREWn";
            string orderInfo = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string returnUrl = "https://localhost:44374/Payments/MoMoNreturnUrl";
            string notifUrl = "https://localhost:44374/Payments/MoMonotifUrl";

            string amount = "1000";
            string orderId = orderIdMoMo.ToString();
            string requestId = "8880";
            string extraData = "";

            string rawHash =
                "partnerCode=" + partnerCode +
                "&accessKey=" + accessKey +
                "&requestId=" + requestId +
                "&amount=" + amount +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&returnUrl=" + returnUrl +
                "&notifyUrl=" + notifUrl +
                "&extraData=" + extraData;
            Session["text-check"] = rawHash;
            MoMoSecurity crypto = new MoMoSecurity();

            string signature = crypto.signSHA256(rawHash, secretKey);
            JObject message = new JObject
                {
                   {"partnerCode" , partnerCode},
                   {"accessKey" , accessKey},
                   {"requestId" , requestId},
                   {"amount" , amount},
                   {"orderId" , orderId},
                   {"orderInfo" , orderInfo},
                   {"returnUrl" , returnUrl},
                   {"notifyUrl" , notifUrl},
                   {"requestType","captureMoMoWallet"},
                   {"signature" , signature }
                };
            Session["text-check1"] = message;
            string responseFromMoMo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());
            Session["text-check2"] = responseFromMoMo;
            JObject jmessage = JObject.Parse(responseFromMoMo);
            Session["text-check3"] = jmessage;
            Response.Redirect(jmessage.GetValue("payUrl").ToString());
        }
    }
}