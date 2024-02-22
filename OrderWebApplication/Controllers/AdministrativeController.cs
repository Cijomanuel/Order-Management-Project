using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderWebApplication.Entity.ViewModels;

namespace OrderWebApplication.Controllers
{
    public class AdministrativeController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AdministrativeController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole user = new IdentityRole
                {
                    Name = model.Name,
                };
                var result = await roleManager.CreateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }
        public IActionResult ListOfRoles()
        {
            return View();

        }
        [HttpPost]
        public async Task<JsonResult> ListJson()
        {
            var roles =  roleManager.Roles;

            return Json(new { data = roles });
        }
        [HttpGet]
        public IActionResult AccesDenied()
        {
            return View();

        }
    }


}
