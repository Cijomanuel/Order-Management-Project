using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    [Table("tbl_Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ? ProductName { get; set; }
        [Required]
        public string ? ProductType { get; set; }
        [Required]
        public string ? Unit { get; set; }
        [Required]
        public int PurchaseUnitPrice { get; set; }
        [Required]
        public int SellingUnitPrice { get; set; }
        [Required]
        public int Avaliable { get; set; }

        
    }
}
