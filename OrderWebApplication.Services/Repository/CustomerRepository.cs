using Microsoft.EntityFrameworkCore;
using OrderWebApplication.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext context;

        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                context.Add(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<Customer> FindCustomerByNumber(string number)
        {
            try
            {
                return context.Customers.Where(x => x.Mobile==number).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                return await context.Customers.ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                return context.Customers.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                context.Customers.Update(customer);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
