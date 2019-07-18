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
    public class CustomersController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET: Customers
        public ActionResult Index(string field, string sortOrder, Customer formSearch)
        {
            //See if searchString is being used. If so then we will return a like clause for the view.
            //if (formSearch != null)
            //{
            //    return View(db.Customers.Where(x => x.ContactName.Contains(formSearch.ContactName))); 
            //}

            //Viewbags for the sorting either Ascending or Descending.
            ViewBag.CompanyNameSort = sortOrder == "compNameAsc" ? "compNameDesc" : "compNameAsc";
            ViewBag.ContactNameSort = sortOrder == "nameAsc" ? "nameDesc" : "nameAsc";
            ViewBag.TitleNameSort = sortOrder == "titleNameAsc" ? "titleNameDesc" : "titleNameAsc";
            ViewBag.AddressSort = sortOrder == "addressAsc" ? "addressDesc" : "addressAsc";
            ViewBag.CitySort = sortOrder == "cityAsc" ? "cityDesc" : "cityAsc";
            ViewBag.RegionSort = sortOrder == "regionAsc" ? "regionDesc" : "regionAsc";
            ViewBag.PostSort = sortOrder == "postAsc" ? "postDesc" : "postAsc";

            if (String.IsNullOrEmpty(field))
                return View(db.Customers.ToList());
            else
            {
                switch (sortOrder)
                {
                    case "compNameAsc":
                        return View(db.Customers.OrderBy(x => x.CompanyName).ToList());
                    case "compNameDesc":
                        return View(db.Customers.OrderByDescending(x => x.CompanyName).ToList());
                    case "nameAsc":
                        return View(db.Customers.OrderBy(x => x.ContactName).ToList());
                    case "nameDesc":
                        return View(db.Customers.OrderByDescending(x => x.ContactName).ToList());
                    case "titleNameAsc":
                        return View(db.Customers.OrderBy(x => x.ContactTitle).ToList());
                    case "titleNameDesc":
                        return View(db.Customers.OrderByDescending(x => x.ContactTitle).ToList());
                    case "addressAsc":
                        return View(db.Customers.OrderBy(x => x.Address).ToList());
                    case "addressDesc":
                        return View(db.Customers.OrderByDescending(x => x.Address).ToList());
                    case "cityAsc":
                        return View(db.Customers.OrderBy(x => x.City).ToList());
                    case "cityDesc":
                        return View(db.Customers.OrderByDescending(x => x.City).ToList());
                    case "regionAsc":
                        return View(db.Customers.OrderBy(x => x.Region).ToList());
                    case "regionDesc":
                        return View(db.Customers.OrderByDescending(x => x.Region).ToList());
                    case "postAsc":
                        return View(db.Customers.OrderBy(x => x.PostalCode).ToList());
                    case "postDesc":
                        return View(db.Customers.OrderByDescending(x => x.PostalCode).ToList());
                    default:
                        //Do nothing.
                        break;
                }
                return View(db.Customers.OrderByDescending(x => x.CompanyName).ToList());
            }
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CustomerPartial", customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CompanyName,ContactName,ContactTitle,Address,City,Region,PostalCode,Country,Phone,Fax")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
