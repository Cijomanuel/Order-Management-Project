using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Services.Repository;

namespace OrderWebApplication.Controllers
{

    [Authorize(Policy = "SalesRolePolicy")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository repository;

        public CustomerController(ICustomerRepository repository)
        {
            this.repository = repository;
        }
        // GET: Index page to show all customers
        [HttpGet]
        public async Task<ActionResult> Index()
        {

            var customers = await repository.GetAllCustomers();
            return View(customers);
        }


        // Action method to find the customer details
        [HttpPost]
        public JsonResult FindCustomer(string mobileNumber)
        {
            var customer = repository.FindCustomerByNumber(mobileNumber);
            return Json(new { data = customer });
        }

        // GET: Action method to create a customer

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Action method to create a customer

        [HttpPost]
        public ActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                repository.AddCustomer(customer);
            }
            return RedirectToAction("Index", "Order");
        }

        // GET: Action method to delete a customer

        public ActionResult Edit(int id)
        {
            var customer = repository.GetCustomerById(id);
            return View(customer);
        }

        // POST: Action method to edit a customer

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateCustomer(customer);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Action method to delete a customer
        public ActionResult Delete(int id)
        {
            return View();
        }

        //// POST: CustomerController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
