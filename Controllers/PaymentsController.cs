using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoMo;
using WebSpecialProject.Models;

namespace WebSpecialProject.Controllers
{
    public class PaymentsController : Controller    
    {
        private WebProjectEntities db = new WebProjectEntities();

        // GET: Payments    
        //returnUrl
        public void MoMoNreturnUrl()
        {
            //get Signature
            string param = Request.QueryString.ToString()
                .Substring(0, Request.QueryString.ToString().IndexOf("signature") - 1);
            param = Server.UrlDecode(param);
            MoMoSecurity cryoto = new MoMoSecurity();
            string secretKey ="&nDKAK4Sx6v4o9vJ9lKuZ9qHmwnePREWn";
            string signature = cryoto.signSHA256(param, secretKey);

            //get Order 
            string orderId = Request.QueryString.ToString()
                .Substring(Request.QueryString.ToString().IndexOf("orderId") + 8 ,
                 Request.QueryString.ToString().IndexOf("orderInfo") - Request.QueryString.ToString().IndexOf("orderId") - 9 
                );
            Session["text-checkorder"] = orderId;
            int numberorderId = Int32.Parse(orderId);


            


            if (signature != Request["signature"].ToString())
            {
                db.Orders.Find(numberorderId).Status = "FailtPayMomo";
                db.SaveChanges();
                Session["text-check"] ="Thông tin không hợp lệ!";
                StatusPayment("Thông tin không hợp lệ!");
            }
            if (!Request.QueryString["errorCode"].Equals("0"))
            {
                db.Orders.Find(numberorderId).Status = "FailtPayMomo";
                db.SaveChanges();
                Session["text-check"] ="Thanh toán thất bại!";
                StatusPayment( "Thanh toán thất bại!");
            }
            else
            {
                db.Orders.Find(numberorderId).Status = "PayedMomo";
                db.SaveChanges();
                Session["text-check"] ="Thanh toán thành công!";
                StatusPayment("Thanh toán thành công!");
            }
        }
        //notifUrl
 
        public JsonResult MoMonotifUrl()
        {
            string secretKey = "nDKAK4Sx6v4o9vJ9lKuZ9qHmwnePREWn";
            Session["text-checkorder"] =  "went to back-end";

            string param ="";
            param =
               "partner_code=" + Request["partner_code"] +
               "&access_key=" + Request["access_key"] +
               "&amount=" + Request["amount"] +
               "&order_id=" + Request["order_id"] +
               "&order_info=" + Request["order_info"] +
               "&order_type=" + Request["order_type"] +
               "&transaction_id=" + Request["transaction_id"] +
               "&message=" + Request["message"] +
               "&respone_time=" + Request["respone_time"] +
               "&status_code=" + Request["status_code"];
            param = Server.UrlDecode(param);
            MoMoSecurity crypto = new MoMoSecurity();
            string status_code = Request["status_code"].ToString();
            string signature = crypto.signSHA256(param, secretKey);
            if (signature != Request["signature"].ToString())
            {
                //tcap nhat don hang that bai vi chu ky khong hop le 
                Session["text-checkorder"] = "went to back-end1";
            }

            if (status_code != "0")
            {

                Session["text-checkorder"] = "went to back-end2";
            }
            else
            {

                Session["text-checkorder"] = "went to back-end3";
            }
               Session["text-checkorder"] = "went to back-end4";
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public void StatusPayment(string StatusInfo)
        {
            Response.Redirect("~/Order/Index");
        }
    }
}