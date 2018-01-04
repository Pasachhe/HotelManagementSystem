using HotelManagementSystem.Models;
using HotelManagementSystem.Repository;
using HotelManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository adminRepo;

        public AdminController()
        {
            adminRepo = new AdminRepository();
        }

        // GET: Admin
        public ActionResult ProfileOfAdmin()
        {
            int id = Convert.ToInt32(Session["Id"]);

            var admin = adminRepo.GetstudentById(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);

        }
        public ActionResult Index()
        {
            var adminList = adminRepo.GetAll().Select(x => new AdminViewModel
            {

                AdminName = x.AdminName,
                AdminEmail = x.AdminName,
                AdminId = x.AdminId,
                AdminPassword = x.AdminPassword,


            });
            return View(adminList);


        }
        [HttpPost]
        public ActionResult Create(AdminViewModel adminviewmodel)
        {
            var admin = new Admin(adminviewmodel);


            adminRepo.Add(admin);



            return View(admin);

        }
        public ActionResult Hotels()
        {
            return RedirectToAction("Hotels", "Hotels");
        }

    }
}