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
    public class StoreController : Controller
    {
        private WebProjectEntities db = new WebProjectEntities();

        // GET: Store
        public ActionResult Index()
        {
            var productToSells = db.ProductToSells.Include(p => p.TypeOfProduct);
            return View(productToSells.ToList());
        }

        [HttpPost]
        public ActionResult AddToCart(FormCollection form)
        {
            ProductOnCart productOnCart = new ProductOnCart();

            productOnCart.IdProduct = Int32.Parse(form["IdProduct"]);
            productOnCart.IdCart = 1;

            if (ModelState.IsValid)
            {
                db.ProductOnCarts.Add(productOnCart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCart = new SelectList(db.Carts, "ID", "ID", productOnCart.IdCart);
            return View(productOnCart);
        }

        // GET: Store/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductToSell productToSell = db.ProductToSells.Find(id);
            if (productToSell == null)
            {
                return HttpNotFound();
            }
            return View(productToSell);
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            ViewBag.IDType = new SelectList(db.TypeOfProducts, "ID", "TypeName");
            return View();
        }

        // POST: Store/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductName,Price,IDType,QuanlityRemain,Description,Image,Origin")] ProductToSell productToSell)
        {
            if (ModelState.IsValid)
            {
                db.ProductToSells.Add(productToSell);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDType = new SelectList(db.TypeOfProducts, "ID", "TypeName", productToSell.IDType);
            return View(productToSell);
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductToSell productToSell = db.ProductToSells.Find(id);
            if (productToSell == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDType = new SelectList(db.TypeOfProducts, "ID", "TypeName", productToSell.IDType);
            return View(productToSell);
        }

        // POST: Store/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductName,Price,IDType,QuanlityRemain,Description,Image,Origin")] ProductToSell productToSell)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productToSell).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDType = new SelectList(db.TypeOfProducts, "ID", "TypeName", productToSell.IDType);
            return View(productToSell);
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductToSell productToSell = db.ProductToSells.Find(id);
            if (productToSell == null)
            {
                return HttpNotFound();
            }
            return View(productToSell);
        }

        // POST: Store/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductToSell productToSell = db.ProductToSells.Find(id);
            db.ProductToSells.Remove(productToSell);
            db.SaveChanges();
            return RedirectToAction("Index");
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
