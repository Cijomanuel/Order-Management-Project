using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Entity.ViewModels;
using OrderWebApplication.Services.Repository;

namespace OrderWebApplication.Controllers
{

    [Authorize(Policy = "ProductRolePolicy")]
    public class ProductController : Controller
    {
        private readonly IProductRepository repository;

        public ProductController(IProductRepository repository)
        {
            this.repository = repository;
        }
        //Action method to display the products page

        public async Task<IActionResult> ShowProducts()
        {
            ProductSearchViewModel productSearchViewModel = null;
            var products = await repository.GetAllProducts(productSearchViewModel);
            var productTypes = products.DistinctBy(x => x.ProductType).Select(x => new Product { ProductType = x.ProductType });
            ViewBag.ProductTypes = productTypes;
            ViewBag.ProductNames = products.Select(x => new Product { ProductName = x.ProductName });
            ViewBag.ProductUnits = products.DistinctBy(x => x.Unit).Select(x => new Product { Unit = x.Unit });
            ViewBag.Products = products;
            return View();
        }

        //Action method to pass json data to showproducts page based on the search inputs


        [HttpPost]
        public async Task<JsonResult> ListJson(ProductSearchViewModel productSearchObject)
        {
            var products = await repository.GetAllProducts(productSearchObject);

            return Json(new { data = products });
        }

        //Action method to add new product
        [HttpGet]
        public IActionResult Create_Product()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create_Product(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.AddProduct(product);
            }
            return RedirectToAction(nameof(ShowProducts));
        }
        //Action method to edit product
        [HttpGet]
        public IActionResult ProductEdit(int id)
        {

            var product = repository.GetProductById(id);
            return View(product);
        }

        //Action method to edit product
        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateProduct(product);
            }

            return RedirectToAction(nameof(ShowProducts));
        }

        //Action method for handling product update required on purchase and sales
        [HttpPost]
        public string ProductUpdate(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateProduct(product);
            }

            return "Success";
        }
        //Action method to delete product
        [HttpGet]
        public IActionResult ProductDelete(int id)
        {
            var product = repository.GetProductById(id);

            return View(product);
        }

        //Action method to delete product
        [HttpPost]
        [ActionName("ProductDelete")]
        public IActionResult PostDeletePost(int id)
        {

            repository.DeleteProduct(id);
            return RedirectToAction(nameof(ShowProducts));
        }
    }
}
