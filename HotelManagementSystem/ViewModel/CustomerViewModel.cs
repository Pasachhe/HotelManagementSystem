using HotelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelManagementSystem.ViewModel
{
    public class CustomerViewModel
    {
      

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerContact { get; set; }
        public string Email { get; set; }
        public DateTime? CheckedIn { get; set; }
        public DateTime? CheckedOut { get; set; }

     
    }
}