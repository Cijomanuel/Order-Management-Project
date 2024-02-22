using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    [Table("tbl_Vendor")]
    public class Vendor
    {
        public int VendorId { get; set; }
        [Required]
        public string VendorName { get; set; }
        [Required]
        public string Contact1 { get; set; }
        public string? Contact2 { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
