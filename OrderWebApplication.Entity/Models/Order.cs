using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    [Table("tbl_Order")]

    //order model , Here order is the subclass of order details

    public class Order
    {
        [Key]
        [Display(Name = "Order Id")]
        public string ? OrderId { get; set; }

        public OrderDetail ? orderDetails { get; set; }

        public int OrderDetailId { get; set; }
        public string ? ProductName { get; set; }
        public int ProductId { get; set; }

        public int Qty { get; set; }

        public int Price { get; set; }
        public int  UnitPrice { get; set; }
    }
}
