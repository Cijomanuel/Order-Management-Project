using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    [Table("tbl_PurchaseDetails")]

    //purchase details model , Here purchase details is the parent class of purchase

    public class PurchaseDetail
    {
        public PurchaseDetail()
        {
            Purchases = new HashSet<Purchase>();
        }
        [Key]
        public int PurchaseDetailsId { get; set; }
        public string? PurchaseDetailsUniqueId { get; set; }
        public int? VendorId { get; set; }
        public string? VendorName { get; set; }
        public int? PurchaseAmount { get; set; }
        public DateTime? PurchaseDatetime { get; set; }
        public string? AdminUserId { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
