using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.ViewModel
{
    public class AdminViewModel
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
    }
}