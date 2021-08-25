using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSpecialProject.Models;


namespace WebSpecialProject.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        WebProjectEntities dbcontext = new WebProjectEntities();

        // GET: Admin/AdminLogin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Login(String taikhoan, String matkhau)
        {
            var search = dbcontext.Users.Where(m => m.UserName == taikhoan && m.PassWord == matkhau).FirstOrDefault();
            if (search != null)
            {
                Session["UserAdmin"] = "112311111123";// search.UserName;
                Session["UserIDCTM"] = search.ID;
                Session["CustomerName"] = search.Name;


                Response.Redirect("~/Admin/AdminHome/Index");
            }
            else
            {
                Response.Redirect("~/Admin/AdminLogin/Index");
            }
        }

        public void Logout()
        {
            Session["UserAdmin"] = "";
            Response.Redirect("~/Admin/AdminLogin/Index");
        }
    }
}