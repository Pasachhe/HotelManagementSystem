using HotelManagementSystem.Models;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        private HotelManageEntities db = new HotelManageEntities();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var emp = db.Employees.Where(x => x.Email.Equals(loginViewModel.Email) && x.Password.Equals(loginViewModel.Password)).FirstOrDefault();
                if (emp == null)
                {
                    ModelState.AddModelError("", "Wrong email or password");
                    return View(loginViewModel);
                }

                Session["Id"] = emp.EmployeeId;
                if (emp.Post == "Owner" || emp.Post == "Manager")
                {


                    return RedirectToAction("Manage", "Employee");
                }
                if (emp.Post == "Receptionist")
                {

                    return RedirectToAction("Reception", "Employee");

                }
            }

            return View(loginViewModel);
        }


        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var admin = db.Admins.Where(x => x.AdminEmail.Equals(loginViewModel.Email) && x.AdminPassword.Equals(loginViewModel.Password)).FirstOrDefault();
                if (admin == null)
                {
                    ModelState.AddModelError("", "Wrong email or password");
                    return View(loginViewModel);
                }

                Session["Id"] = admin.AdminId;

                return RedirectToAction("ProfileOfAdmin", "Admin");

            }

            return View(loginViewModel);
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