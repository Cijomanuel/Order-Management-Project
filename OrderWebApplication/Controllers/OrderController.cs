using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Entity.ViewModels;
using OrderWebApplication.Services.Repository;
using System.Security.Claims;

namespace OrderWebApplication.Controllers
{
    [Authorize(Policy = "SalesRolePolicy")]
    public class OrderController : Controller
    {
        private readonly IProductRepository pRepository;

        public IOrderRepository repository { get; }
        public OrderController(IOrderRepository repository , IProductRepository pRepository)
        {
            this.repository = repository;
            this.pRepository = pRepository;
        }

        //Index Page

        public async Task<IActionResult> Index()
        {
            ProductSearchViewModel productSearchViewModel = null;
            var products = await pRepository.GetAllProducts(productSearchViewModel);
            var productTypes = products.DistinctBy(x => x.ProductType).Select(x => new Product { ProductType = x.ProductType });
            ViewBag.ProductTypes = productTypes;
            ViewBag.ProductNames = products.Select(x => new Product { ProductName = x.ProductName });
            ViewBag.Products = products;
            ViewBag.LoggedUserId = User.FindFirstValue(ClaimTypes.Name);
            return View();
        }

        // Data retrival Action method, used by jquery function(inside Index)

        [HttpPost]
        public async Task<JsonResult> ListJson()
        {
            ProductSearchViewModel productSearchViewModel = null;
            var products = await pRepository.GetAllProducts(productSearchViewModel);

            return Json(new { data = products });
        }

        //Method to Save order detail regarding user and retriving id

        [HttpPost]
        public int SaveAndGet(OrderDetail orderd)
        {
            var str = repository.AddOrderDetails(orderd);

            return str;
        }

        //Method for Saving each order

        [HttpPost]
        public void OrderSubmit(Order order)
        {
            repository.AddOrder(order);
        }

        
    }

}
