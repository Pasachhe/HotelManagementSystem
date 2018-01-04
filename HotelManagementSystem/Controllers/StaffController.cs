using HotelManagementSystem.Models;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class StaffController : Controller
    {

        private HotelManageEntities db = new HotelManageEntities();

        //employee list
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["HotelId"]);
            var employee = db.Employees.Where(x => x.HotelId.Equals(id)).ToList();
            return View(employee);
        }

        //Addition of new employee
        public ActionResult AddNew()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddNew(EmployeeViewModel employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                employeeViewModel.HotelId = Convert.ToInt32(Session["HotelId"]);
                Employee employee = new Employee(employeeViewModel);
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeViewModel);

        }
        //remove the employee
        public ActionResult Remove(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();

            return RedirectToAction("Hotels");
        }

        public ActionResult Back()
        {
            Session["HotelId"] = null;
            return RedirectToAction("Manage", "Employee");
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