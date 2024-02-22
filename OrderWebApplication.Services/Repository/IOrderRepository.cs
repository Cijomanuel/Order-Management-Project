using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderWebApplication.Entity.Models;

namespace OrderWebApplication.Services.Repository
{
    public interface IOrderRepository
    {
        Task<List<OrderDetail>> GetOrders();
        void AddOrder(Order order);

        int AddOrderDetails(OrderDetail orderd);

    }
}
