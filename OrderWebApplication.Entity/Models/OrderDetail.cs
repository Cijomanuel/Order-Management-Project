using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    [Table("tbl_OrderDetails")]

    //order details model , Here order details is the parent class of order

    public class OrderDetail
    {

        public ICollection<Order> Orders { get; set; }

        public OrderDetail()
        {
            Orders = new HashSet<Order>();
        }
        [Key]
        [Display(Name = "Order detail Id")]
        public int OrderDetailId { get; set; }

        [DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ? OrderDate { get; set; }
        public string ? UserIdMobile { get; set; }
        public int TotalAmount { get; set; }

        public string ?  Address { get; set; }
        public string ? AdminId { get; set; }
    }
}
