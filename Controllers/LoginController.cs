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
        public void Login(String taikhoan, String matkhau)
        {          
            var search = dbcontext.Users.Where(m => m.UserName == taikhoan && m.PassWord == matkhau).FirstOrDefault();
            if (search != null)
            {   
                Session["CustomerName"] = search.UserName;
                Session["UserIDCTM"] = search.ID;
                Session["Status"] = "logedIn";
                Session["AmmountOnCart"] = dbcontext.ProductOnCarts.Where( p => p.Status == "InProcess").Count();

                Response.Redirect("~/Home/Index");
            }
            else
            {
                Response.Redirect("~/Login/Login");
            }
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