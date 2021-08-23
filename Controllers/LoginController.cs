using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSpecialProject.Models;


namespace WebSpecialProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        WebProjectEntities dbcontext = new WebProjectEntities();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(String taikhoan, String matkhau)
        {
                
            var search = dbcontext.Users.Where(m => m.UserName == taikhoan && m.PassWord == matkhau).FirstOrDefault();
            if (search != null)
            {   
                Session["UserCTM"] = search.UserName;
                Session["UserIDCTM"] = search.ID;
                Session["UserCTMName"] = search.Name;
                Session["UserCTMAddress"] = search.Address;
                Session["UserCTMPhoneNumber"] = search.SDT;
                Session["AmmountOnCart"] = dbcontext.ProductOnCarts.Where( p => p.Status == "InProcess").Count();

                Response.Redirect("~/Home/Index");
            }
            else
            {
                return View("Login");
            }
            Session["Status"] = "1";
            Session["CustomerName"] = search.Name;
            return View();
        }

        public ActionResult Logout()
        {
            Session["Status"] = null;
            Session["CustomerName"] = null;
            Session["UserCTM"] = null;
            Session["UserIDCTM"] = null;
            Session["UserCTMPhoneNumber"] = null;
            Response.Redirect("~/Home/Index");
            return null;
        }
    }
}