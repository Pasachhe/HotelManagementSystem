using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class CheckController : Controller
    {

        private HotelManageEntities db = new HotelManageEntities();

        public ActionResult ReservationList()
        {
            //    int id =Convert.ToInt32(Session["Id"]);

            var customer = db.Customers.Where(x => x.CheckedIn.Equals(null)).ToList();
            return View(customer);

        }

        public ActionResult CustomerList()
        {
            var customer = db.Customers.Where(x => x.CheckedIn != null).ToList();
            return View(customer);
        }

        public ActionResult CheckIn(int id)
        {
            var customer = db.Customers.Where(x => x.CustomerId.Equals(id)).FirstOrDefault();
            customer.CheckedIn = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("CustomerList");

        }

        public ActionResult CheckOut(int id)
        {
            Session["BillId"] = id;
            var customer = db.Customers.Where(x => x.CustomerId.Equals(id)).FirstOrDefault();
            Session["BillUpto"] = customer.CheckedOut = DateTime.Now;
            db.SaveChanges();

            return RedirectToAction("PayBill", "Bill");

        }

        public ActionResult Bills(int id)
        {
            Session["BillId"] = id;

            Session["BillUpto"] = DateTime.Now;


            return RedirectToAction("ViewBill", "Bill");

        }

        public ActionResult Back()
        {

            return RedirectToAction("Reception", "Employee");
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