using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.Repository
{
    public interface IAdminRepository
    {
        void Add(Admin admin);
        List<Admin> GetAll();
        Admin GetstudentById(int adminId);
        
    }
    public class AdminRepository : IAdminRepository
    {
        private HotelManageEntities db = new HotelManageEntities();

        public void Add(Admin admin)
        {
            db.Admins.Add(admin);
        }

        public List<Admin> GetAll()
        {
            return db.Admins.ToList();
        }

        public Admin GetstudentById(int adminId)
        {
            return db.Admins.Find(adminId);
        }
    }
}