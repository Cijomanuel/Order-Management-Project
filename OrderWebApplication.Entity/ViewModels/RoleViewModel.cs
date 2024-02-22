using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.ViewModels
{
    public class RoleViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
