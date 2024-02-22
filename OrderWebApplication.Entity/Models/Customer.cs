using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    [Table("tbl_Customer")]
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }
        public string ? Address { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string ? Email { get; set; }
        public int TranscationNo { get; set; }
    }
}
