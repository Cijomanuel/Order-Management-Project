using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Entity.Models
{
    public class ClaimStore
    {
        public int ClaimId { get; set; }
        public string ? ClaimType { get; set; }
        public string ? ClaimValue { get; set; }
    }
}

