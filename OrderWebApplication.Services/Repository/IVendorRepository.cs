using OrderWebApplication.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetAllVendors();
        Vendor GetVendorById(int id);
        void AddVendor(Vendor vendor);
        void UpdateVendor(Vendor vendor);

        void DeleteVendor(int id);
    }
}
