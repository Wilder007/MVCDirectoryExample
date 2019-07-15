using DirectoryExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DirectoryExample.Controllers
{
    public class HomeController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexCustomers()
        {
            return View(db.Customers.ToList());
        }

        public ActionResult IndexProducts()
        {
            return View(db.Products.ToList());
        }

        public ActionResult IndexEmployees()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}