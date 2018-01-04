using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HotelManagementSystem.Models;
using HotelManagementSystem.ViewModel;

namespace HotelManagementSystem.Controllers
{
    public class HotelsController : Controller
    {
        private HotelManageEntities db = new HotelManageEntities();


        public ActionResult Hotels()
        {
            var hotel = db.Hotels.ToList();
            return View(hotel);
        }
        public ActionResult Owner(int? id)
        {
            Session["hotelId"] = id;
            return View();
        }
        [HttpPost]
        public ActionResult Owner(EmployeeViewModel ownerviewmodel)
        {
            ownerviewmodel.HotelId = Convert.ToInt32(Session["hotelId"]);
            Employee owner = new Employee(ownerviewmodel);
            db.Employees.Add(owner);
            db.SaveChanges();
            Session["hotelId"] = null;
            return View();

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Hotels");
            }

            return View(hotel);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Hotels");
            }
            return View(hotel);
        }


        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
            var employee = db.Employees.Where(x => x.HotelId.Equals(id)).ToList();
            foreach (var item in employee)
            {
                db.Employees.Remove(item);
            }
            var customer = db.Customers.Where(x => x.HotelId.Equals(id)).ToList();
            foreach (var item in customer)
            {
                db.Customers.Remove(item);
            }
            var room = db.Rooms.Where(x => x.HotelId.Equals(id)).ToList();
            foreach (var item in room)
            {
                db.Rooms.Remove(item);
            }
            var record = db.Records.Where(x => x.HotelId.Equals(id)).ToList();
            foreach (var item in record)
            {
                db.Records.Remove(item);
            }


            db.SaveChanges();
            return RedirectToAction("Hotels");
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
