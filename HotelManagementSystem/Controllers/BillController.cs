using HotelManagementSystem.Models;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class BillController : Controller
    {
        private HotelManageEntities db = new HotelManageEntities();

        public int TimeSpan(int id, DateTime BillUpto, DateTime BillFrom)
        {
            TimeSpan stayedTime = BillUpto.Subtract(BillFrom);
            int days = stayedTime.Days;
            int hours = stayedTime.Hours;
            if (days == 0 || hours < 24)
            {
                days += 1;
            }
            return (days);
        }


        // GET: Bill
        public ActionResult ViewBill()
        {
            int id = Convert.ToInt32(Session["BillId"]);

            DateTime BillUpto = Convert.ToDateTime(Session["BillUpto"]);



            var customer = db.Customers.Where(x => x.CustomerId.Equals(id)).FirstOrDefault();
            DateTime BillFrom = Convert.ToDateTime(customer.CheckedIn);
            int days = TimeSpan(id, BillUpto, BillFrom);
            var room = db.Rooms.Where(x => x.RoomId.Equals(customer.RoomId) && x.HotelId.Equals(customer.HotelId)).FirstOrDefault();

            Session["days"] = days;
            Session["PriceOfRoom"] = db.Rooms.Where(x => x.RoomId.Equals(customer.RoomId) && x.HotelId.Equals(customer.HotelId)).FirstOrDefault().Price;
            Session["TotalPrice"] = (days * room.Price);

            return View();

        }

        public ActionResult PayBill()
        {
            int id = Convert.ToInt32(Session["BillId"]);

            DateTime BillUpto = Convert.ToDateTime(Session["BillUpto"]);

            var customer = db.Customers.Where(x => x.CustomerId.Equals(id)).FirstOrDefault();
            DateTime BillFrom = Convert.ToDateTime(customer.CheckedIn);
            var room = db.Rooms.Where(x => x.RoomId.Equals(customer.RoomId) && x.HotelId.Equals(customer.HotelId)).FirstOrDefault();
            room.Status = "Available";

            int days = TimeSpan(id, BillUpto, BillFrom);

            Session["days"] = days;
            Session["PriceOfRoom"] = db.Rooms.Where(x => x.RoomId.Equals(customer.RoomId) && x.HotelId.Equals(customer.HotelId)).FirstOrDefault().Price;
            Session["TotalPrice"] = (days * room.Price);

            Record record = new Record(customer);
            db.Customers.Remove(customer);
            db.Records.Add(record);
            db.SaveChanges();


            return View();
        }
        public ActionResult Back()
        {
            Session["BillId"] = null;
            Session["BillUpto"] = null;
            Session["days"] = null;
            Session["PriceOfRoom"] = null;
            Session["TotalPrice"] = null;

            return RedirectToAction("CustomerList", "Check");
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