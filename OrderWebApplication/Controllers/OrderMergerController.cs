using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Services.Repository;

namespace OrderWebApplication.Controllers
{

    [Authorize(Policy = "SalesRolePolicy")]
    public class OrderMergeController : Controller
    {
        private readonly IOrderRepository repository;

        public OrderMergeController(IOrderRepository repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        //POST : Action method to get all order-details which are merged with order

        [HttpPost]
        public async Task<JsonResult> GetJson()
        {
            var orders = await repository.GetOrders();

            return Json(new { data = orders });
        }
        
    }
}