using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DirectoryExample.Models;
using System.Data.SqlClient;

namespace DirectoryExample.Controllers
{
    public class EmployeesController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Employee1);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return PartialView("_EmployeePartial",employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employee.ReportsTo);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employee.ReportsTo);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,LastName,FirstName,Title,TitleOfCourtesy,BirthDate,HireDate,Address,City,Region,PostalCode,Country,HomePhone,Extension,Photo,Notes,ReportsTo,PhotoPath")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReportsTo = new SelectList(db.Employees, "EmployeeID", "LastName", employee.ReportsTo);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        public ActionResult ExportEmployees()
        {
            byte[] fileData = null;
            //string filePath = Server.MapPath("~/App_Data/Employees.xlsx");
            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Employees");
                    
                    worksheet.Cells["A1"].LoadFromCollection(db.Employees.ToList(), true);
                    fileData = excelPackage.GetAsByteArray();

                    //Clear buffer stream
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Buffer = true;

                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-length", fileData.Length.ToString());
                    Response.AddHeader("content-dipopsition", "attachment; filename=\"Employees.xlsx\"");
                    Response.OutputStream.Write(fileData, 0, fileData.Length);
                    Response.Flush();
                    Response.Close();
                    //HttpContext.ApplicationInstance.CompleteRequest();
                    
                    //FileInfo fileinfo = new FileInfo(filePath);
                    //excelPackage.SaveAs(fileinfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error exporting Employees. Error: " + ex.ToString());
            }
            
            return RedirectToAction("Index");
        }

        //This function grabs the data from the model and then exports them to excel.
        //[HttpPost, ActionName("ExportEmp")]
        public void ExportEmp()
        {
            try
            {
                var emps = db.Employees.ToList();
                var datapath = Server.MapPath("~/App_Data/Employees.txt");
                FileStream fs = new FileStream(datapath, FileMode.OpenOrCreate);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (Employee emp in emps)
                    {
                        sw.WriteLine(emp.LastName + " " + emp.FirstName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in EmpController ExportEmp. Error:" + ex.ToString());
            }
            RedirectToAction("Index");
        }
    }
}
