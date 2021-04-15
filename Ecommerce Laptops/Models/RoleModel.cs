using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Laptops.Models
{
    public class RoleModel
    {
        [Required]
        [Display(Name ="Role")]
        public string RoleName { get; set; }
    }
}
