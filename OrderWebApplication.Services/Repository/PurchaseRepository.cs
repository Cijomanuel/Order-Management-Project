using Microsoft.EntityFrameworkCore;
using OrderWebApplication.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext context;

        public PurchaseRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void AddPurchase(Purchase purchase)
        {
            try
            {
                context.Add(purchase);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddPurchaseDetails(PurchaseDetail purchased)
        {
            try
            {
                context.Add(purchased);
                context.SaveChanges();

                var id = purchased.PurchaseDetailsId;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PurchaseDetail>> GetPurchases()
        {
            try
            {
                var purchases = await context.PurchaseDetails.Include(p => p.Purchases).ToListAsync();
                return purchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
