using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Entity.ViewModels;
using OrderWebApplication.Services.Repository;
using System.Security.Claims;

namespace OrderWebApplication.Controllers
{
    [Authorize(Policy = "PurchaseRolePolicy")]
    public class PurchaseController : Controller
    {
        private readonly IPurchaseRepository repository;
        private readonly IProductRepository pRepository;
        private readonly IVendorRepository vRepository;

        public PurchaseController(IPurchaseRepository repository, IProductRepository pRepository, IVendorRepository vRepository)
        {
            this.repository = repository;
            this.pRepository = pRepository;
            this.vRepository = vRepository;
        }
        //Index Page

        public async Task<IActionResult> Index()
        {
            ProductSearchViewModel productSearchViewModel = null;
            var products = await pRepository.GetAllProducts(productSearchViewModel);
            var vendors = await vRepository.GetAllVendors();
            var productTypes = products.DistinctBy(x => x.ProductType).Select(x => new Product { ProductType = x.ProductType });
            ViewBag.ProductTypes = productTypes;
            ViewBag.ProductNames = products.Select(x => new Product { ProductName = x.ProductName });
            ViewBag.Products = products;
            ViewBag.VendorNames = vendors.Select(x => new { x.VendorName, x.VendorId });
            ViewBag.LoggedUserId = User.FindFirstValue(ClaimTypes.Name);
            return View();
        }
        [HttpPost]
        public int SaveAndGet(PurchaseDetail purchased)
        {
            var str = repository.AddPurchaseDetails(purchased);

            return str;
        }

        //Method for Saving each order

        [HttpPost]
        public string PurchaseSubmit(Purchase purchase)
        {
            repository.AddPurchase(purchase);
            return "Success";
        }
        //Method to display the joined table with Purchase and Purchase Details
        public IActionResult ShowPurchases()
        {
            return View();
        }

        //Action method to pass json data on all purchases to ShowPurchases page

        [HttpPost]
        public async Task<JsonResult> GetJson()
        {
            var purchases = await repository.GetPurchases();

            return Json(new { data = purchases });
        }
    }
}
