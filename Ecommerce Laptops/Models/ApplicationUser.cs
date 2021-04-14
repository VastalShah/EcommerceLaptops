using Microsoft.AspNetCore.Identity;
using System;

namespace Ecommerce_Laptops.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; internal set; }
    }
}
