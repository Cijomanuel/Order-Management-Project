using Microsoft.EntityFrameworkCore;
using OrderWebApplication.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public class VendorRepository : IVendorRepository
    {
        private readonly AppDbContext context;

        public VendorRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void AddVendor(Vendor vendor)
        {
            try
            {
                context.Add(vendor);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteVendor(int id)
        {
            try
            {
                var vendor = context.Products.Find(id);
                context.Products.Remove(vendor);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Vendor>> GetAllVendors()
        {
            try
            {
                return await context.Vendors.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Vendor GetVendorById(int id)
        {
            try
            {
                return context.Vendors.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateVendor(Vendor vendor)
        {
            try
            {
                context.Vendors.Update(vendor);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
