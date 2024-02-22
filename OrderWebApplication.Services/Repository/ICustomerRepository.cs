using OrderWebApplication.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderWebApplication.Services.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Customer GetCustomerById(int id);

        List<Customer> FindCustomerByNumber(string number);

        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
    }
}
