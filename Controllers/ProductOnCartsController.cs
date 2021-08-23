using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSpecialProject.Models;

namespace WebSpecialProject.Controllers
{
    public class ProductOnCartsController : Controller
    {
        private WebProjectEntities db = new WebProjectEntities();

        // GET: ProductOnCarts
        public ActionResult Index()
        {
            var productOnCarts = db.ProductOnCarts.Include(p => p.Cart);
            return View(productOnCarts.ToList());
        }

        // GET: ProductOnCarts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOnCart productOnCart = db.ProductOnCarts.Find(id);
            if (productOnCart == null)
            {
                return HttpNotFound();
            }
            return View(productOnCart);
        }

        // GET: ProductOnCarts/Create
        public ActionResult Create()
        {
            ViewBag.IdCart = new SelectList(db.Carts, "ID", "ID");
            return View();
        }

        // POST: ProductOnCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,QuantityOnCart,IdCart,Status,IdProduct")] ProductOnCart productOnCart)
        {
            if (ModelState.IsValid)
            {
                db.ProductOnCarts.Add(productOnCart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCart = new SelectList(db.Carts, "ID", "ID", productOnCart.IdCart);
            return View(productOnCart);
        }

        // GET: ProductOnCarts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOnCart productOnCart = db.ProductOnCarts.Find(id);
            if (productOnCart == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCart = new SelectList(db.Carts, "ID", "ID", productOnCart.IdCart);
            return View(productOnCart);
        }

        // POST: ProductOnCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,QuantityOnCart,IdCart,Status,IdProduct")] ProductOnCart productOnCart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productOnCart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCart = new SelectList(db.Carts, "ID", "ID", productOnCart.IdCart);
            return View(productOnCart);
        }

        /*
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductOnCart productOnCart = db.ProductOnCarts.Find(id);
            if (productOnCart == null)
            {
                return HttpNotFound();
            }
            return View(productOnCart);
        }

        // POST: ProductOnCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]*/
        public ActionResult Delete(int id)
        {
            ProductOnCart productOnCart = db.ProductOnCarts.Find(id);
            db.ProductOnCarts.Remove(productOnCart);
            db.SaveChanges();
            Response.Redirect("~/Cart/Index");
            Session["AmmountOnCart"] = db.ProductOnCarts.Where(p => p.Status == "InProcess").Count();
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
