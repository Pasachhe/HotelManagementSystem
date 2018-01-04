using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private HotelManageEntities db = new HotelManageEntities();
        // GET: Staff
        public ActionResult Manage()
        {
            int id = Convert.ToInt32(Session["Id"]);
            if (Session["Id"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            Session["HotelId"] = employee.HotelId;
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);

        }
        public ActionResult Room()
        {

            return RedirectToAction("Index", "Rooms");
        }
        public ActionResult Staff()
        {

            return RedirectToAction("Index", "Staff");

        }



        public ActionResult Reception()
        {
            Session["CustomerId"] = null;
            int id = Convert.ToInt32(Session["Id"]);
            if (Session["Id"] == null)
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

        public ActionResult Customer(string id)
        {
            Session["CustomerId"] = id;
            return RedirectToAction("Customers");
        }

        public ActionResult Customers()
        {
            string id = Session["CustomerId"].ToString();
            if (id == "Reservation")
            {

                return RedirectToAction("ReservationList", "Check");
            }
            else if (id == "Customer")
            {

                return RedirectToAction("CustomerList", "Check");
            }
            return RedirectToAction("Reception");
        }

        public ActionResult Record()
        {
            int id = Convert.ToInt32(Session["HotelId"]);

            var record = db.Records.Where(x => x.HotelId.Equals(id)).ToList();
            return View(record);
        }
        public ActionResult Back()
        {
            int id = Convert.ToInt32(Session["Id"]);
            var post = db.Employees.Where(x => x.EmployeeId.Equals(id)).FirstOrDefault().Post;
            if (post == "Owner" || post == "Manager")
            {

                return RedirectToAction("Manage");
            }
            if (post == "Receptionist")
            {

                return RedirectToAction("Reception");

            }
            return View();
        }


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
            return View(employee);
        }


        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                if (employee.Post == "Owner" || employee.Post == "Manager")
                {

                    return RedirectToAction("Manage");
                }
                if (employee.Post == "Receptionist")
                {

                    return RedirectToAction("Reception");

                }
            }
            return View(employee);
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