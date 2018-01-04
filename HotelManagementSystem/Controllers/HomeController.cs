using HotelManagementSystem.Models;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private HotelManageEntities db = new HotelManageEntities();

        public ActionResult Index()
        {
            Session["HotelId"] = null;
            return View(db.Hotels.ToList());
        }

        public ActionResult session(int id)
        {
            Session["HotelId"] = id;
            return RedirectToAction("HotelRooms");
        }

        public ActionResult HotelRooms()
        {
            int id = Convert.ToInt32(Session["HotelId"]);
            var room = db.Rooms.Where(x => x.HotelId.Equals(id)).ToList();
            return View(room);

        }


        public ActionResult Reserve(int? id)
        {
            TempData["RoomId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult Reserve(CustomerViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                int HotelId = Convert.ToInt32(Session["HotelId"]);
                int RoomId = Convert.ToInt32(TempData["RoomId"]);
                Customer customer = new Customer(cvm);
                customer.HotelId = HotelId;
                customer.RoomId = RoomId;

                var room = db.Rooms.Where(x => x.HotelId.Equals(HotelId) && x.RoomId.Equals(RoomId)).FirstOrDefault();
                if (room.Status.ToString() == "Reserved" || room.Status.ToString() == "Booked")
                {
                    return RedirectToAction("HotelRooms");
                }
                else { room.Status = "Reserved"; }
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("HotelRooms");
            }
            return View(cvm);
        }
        public ActionResult ReservationCancel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ReservationCancel(LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var customer = db.Customers.Where(x => x.Email.Equals(lvm.Email)).FirstOrDefault();
                int id = Convert.ToInt32(Session["HotelId"]);

                if (customer.HotelId != id)
                {
                    return RedirectToAction("HotelRooms");
                }
                var room = db.Rooms.Where(x => x.RoomId.Equals(customer.RoomId)).FirstOrDefault();
                room.Status = "Available";
                db.Customers.Remove(customer);
                db.SaveChanges();
                return RedirectToAction("HotelRooms");
            }
            return View(lvm);

        }


        public ActionResult Login()
        {

            return RedirectToAction("Login", "Login");
        }
        public ActionResult Admin()
        {

            return RedirectToAction("AdminLogin", "Login");
        }
        public ActionResult Logout()
        {
            Session["Id"] = null;
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
