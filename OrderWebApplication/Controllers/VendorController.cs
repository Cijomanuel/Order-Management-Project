using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Services.Repository;

namespace OrderWebApplication.Controllers
{

    [Authorize(Policy = "PurchaseRolePolicy")]
    public class VendorController : Controller
    {
        private readonly IVendorRepository repository;

        public VendorController(IVendorRepository repository)
        {
            this.repository = repository;
        }
        // GET: Action method to view all vendors
        public async Task<ActionResult> Index()
        {
            var vendors = await repository.GetAllVendors();
            return View(vendors);
        }


        // GET: Action method to create new vendors

        public ActionResult Create()
        {
            return View();
        }

        // POST: Action method to create new vendors

        [HttpPost]
        public ActionResult Create(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                repository.AddVendor(vendor);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Action method to edit vendor
        public ActionResult Edit(int id)
        {
            var vendor = repository.GetVendorById(id);

            return View(vendor);
        }

        // POST: Action method to edit vendor

        [HttpPost]
        public ActionResult Edit(Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateVendor(vendor);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Action method to delete vendor

        public ActionResult Delete(int id)
        {
            var vendor = repository.GetVendorById(id);

            return View(vendor);
        }

        // POST: Action method to delete vendor

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteVendor(int id)
        {

            repository.DeleteVendor(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
