using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderWebApplication.Entity.Models;
using OrderWebApplication.Entity.ViewModels;

namespace OrderWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext dbContext;

        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDbContext dbContext)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
            this.dbContext = dbContext;
        }

        //GET: Login Page

        public IActionResult Login()
        {
            return View();
        }

        //POST: Login Page

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Order");

                }
                else
                {
                    ModelState.AddModelError("IncorrectPassword", "The user name or password provided is invalid.");
                }
            }
            return View();
        }

        //GET: Register Page

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        //POST: Register Page

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user, model.Password);

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

        //POST: Logout Page

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        //GET : Action method to display users to handle claims

        [Authorize(Policy = "ClaimRolePolicy")]
        [HttpGet]
        public IActionResult ListOfUsers()
        {
            var users = UserManager.Users;
            if (User.Identity.IsAuthenticated)
            {
                return View(users);

            }
            return View(users);
        }



        //GET : Action method to handle claims

        [HttpGet]
        [Authorize(Policy = "ClaimRolePolicy")]
        public async Task<IActionResult> ManageClaims(string Id)
        {
            var user = await UserManager.FindByIdAsync(Id);

            ViewBag.UserId = user.Id;

            ViewBag.UserName = user.UserName;

            var UserClaim = await UserManager.GetClaimsAsync(user);

            var model = new UserClaimViewModel
            { UserId = Id };



            var claimStore = dbContext.ClaimStores.ToList();
            foreach (var claim in claimStore)
            {
                UserClaim uClaims = new UserClaim
                {
                    ClaimType = claim.ClaimType,
                };
                if (UserClaim.Any(c => c.Type == claim.ClaimType))
                {
                    uClaims.isSelected = true;
                }
                model.Claims.Add(uClaims);
            }

            return View(model);
        }


        //POST : Action method to handle claims

        [HttpPost]

        [Authorize(Policy = "ClaimRolePolicy")]
        public async Task<IActionResult> ManageClaims(UserClaimViewModel model)
        {
            var user = await UserManager.FindByIdAsync(model.UserId);

            var UserClaims = await UserManager.GetClaimsAsync(user);

            var result = await UserManager.RemoveClaimsAsync(user, UserClaims);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Can't Remove existing claims");
                return View(model);
            }

            result = await UserManager.AddClaimsAsync(user, model.Claims.Where(c => c.isSelected).Select(e => new System.Security.Claims.Claim(e.ClaimType, e.ClaimType)));

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Can't remove Existing claims");
                return View();
            }
            return RedirectToAction(nameof(ListOfUsers));
        }

        //GET : Action method to reset password

        [HttpGet]
        public IActionResult Manageuser()
        {
            return View();
        }

        //POST : Action method to reset password


        [HttpPost]
        public async Task<IActionResult> Manageuser(ChangepasswordViewModel viewModel)
        {
            var user = await UserManager.GetUserAsync(User);

            var result = await UserManager.ChangePasswordAsync(user, viewModel.OldPassword, viewModel.NewPassword);

            if (!result.Succeeded)
            {

                ModelState.AddModelError("IncorrectPassword", "The password provided is incorrect.");
                return View();
            }
            return RedirectToAction("Logout", "Account");
        }
    }
}

