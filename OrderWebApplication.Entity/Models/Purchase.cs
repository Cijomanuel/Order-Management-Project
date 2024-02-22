using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    [Table("tbl_Purchase")]

    //purchase model , Here purchase is the subclass of purchase details

    public class Purchase
    {

        [Key]
        public string PurchaseId { get; set; } = null!;
        public int? PurchaseDetailsId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }

        public virtual PurchaseDetail? PurchaseDetailIsdNavigation { get; set; }
    }
}
