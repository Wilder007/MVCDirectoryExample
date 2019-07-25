using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DirectoryExample.Models;

namespace DirectoryExample.Controllers
{
    public class Order_Details_ExtendedController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET: Order_Details_Extended
        public ActionResult Index()
        {
            return View(db.Order_Details_Extendeds.ToList());
        }

        // GET: Order_Details_Extended/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details_Extended order_Details_Extended = db.Order_Details_Extendeds.Find(id);
            if (order_Details_Extended == null)
            {
                return HttpNotFound();
            }
            return View(order_Details_Extended);
        }

        // GET: Order_Details_Extended/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order_Details_Extended/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,ProductID,ProductName,UnitPrice,Quantity,Discount,ExtendedPrice")] Order_Details_Extended order_Details_Extended)
        {
            if (ModelState.IsValid)
            {
                db.Order_Details_Extendeds.Add(order_Details_Extended);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order_Details_Extended);
        }

        // GET: Order_Details_Extended/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details_Extended order_Details_Extended = db.Order_Details_Extendeds.Find(id);
            if (order_Details_Extended == null)
            {
                return HttpNotFound();
            }
            return View(order_Details_Extended);
        }

        // POST: Order_Details_Extended/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,ProductID,ProductName,UnitPrice,Quantity,Discount,ExtendedPrice")] Order_Details_Extended order_Details_Extended)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order_Details_Extended).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order_Details_Extended);
        }

        // GET: Order_Details_Extended/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order_Details_Extended order_Details_Extended = db.Order_Details_Extendeds.Find(id);
            if (order_Details_Extended == null)
            {
                return HttpNotFound();
            }
            return View(order_Details_Extended);
        }

        // POST: Order_Details_Extended/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order_Details_Extended order_Details_Extended = db.Order_Details_Extendeds.Find(id);
            db.Order_Details_Extendeds.Remove(order_Details_Extended);
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
