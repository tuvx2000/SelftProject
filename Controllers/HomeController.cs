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
            var list = dbcontext.Supplements.ToList();
            var x = list[0];
            var y = x.FoodName;
            return View(list);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            User user = new User();
            user.UserName = form["tentaikhoan"];
            user.PassWord = form["matkhau"];
            user.Email = form["email"];
            user.Name = form["hoten"];
            user.Address = form["diachi"];
            user.SDT = form["sdt"];

            string inform = "";
            var search = dbcontext.Users.Where(m => m.UserName == user.UserName).FirstOrDefault();
            if (search == null)
            {
                dbcontext.Users.Add(user);
                dbcontext.SaveChanges();
                inform = "Đăng ký thành công. Hãy đăng nhập!";
                ViewBag.Inform = inform;
            }
            else
            {
                inform = "Tên tài khoản đã tồn tại. Vui lòng thử lại! ";
                ViewBag.Inform = inform;
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}