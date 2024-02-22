using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }

        //method to add new orders , Here order is the subclass of order details

        public void AddOrder(Order order)
        {
            try
            {
                context.Add(order);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //method to add new order details

        public int AddOrderDetails(OrderDetail orderd)
        {
            try
            {
                context.Add(orderd);
                context.SaveChanges();

                var id = orderd.OrderDetailId;
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //POST :  method to get all order-details which are merged with order using include

        public async Task<List<OrderDetail>> GetOrders()
        {
            try
            {
                var orders = await context.OrderDetails.Include(o => o.Orders).ToListAsync();
                return orders;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
