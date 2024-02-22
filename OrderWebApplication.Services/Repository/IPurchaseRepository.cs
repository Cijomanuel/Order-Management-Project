using OrderWebApplication.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public interface IPurchaseRepository
    {
        Task<List<PurchaseDetail>> GetPurchases();
        void AddPurchase(Purchase purchase);

        int AddPurchaseDetails(PurchaseDetail purchased);
    }
}
