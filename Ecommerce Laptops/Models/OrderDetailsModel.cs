using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Laptops.Models
{
    public class OrderDetailsModel
    {
        public int Product_id { get; set; }
        public string Order_id { get; set; }
        public string User_id { get; set; }
        public string Product_name { get; set; }
        public string Product_ImageUrl { get; set; }
        public int Product_Price { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
