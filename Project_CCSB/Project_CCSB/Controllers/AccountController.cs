using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Inloggen mislukt");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Register()
        {
            if (!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.User));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    LastName = model.LastName,
                    AccountNumber = model.AccountNumber,
                    City = model.City,
                    Adress = model.Adress,
                    ZipCode = model.ZipCode,
                    BirthDate = model.BirthDate
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                    return RedirectToAction("index", "home");
                }
                // Add all errors to the modelstate
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllUsers()
        {
            // Get all users
            var allUsers = (from user in _db.Users
                            join userRole in _db.UserRoles on user.Id equals userRole.UserId
                            join role in _db.Roles on userRole.RoleId equals role.Id
                            select new UserViewModel
                            {
                                Id = user.Id,
                                Name = string.IsNullOrEmpty(user.MiddleName) ?
                                    user.FirstName + " " + user.LastName :
                                    user.FirstName + " " + user.MiddleName + " " + user.LastName,
                                Email = user.Email,
                                Role = role.Name
                            }).ToList();

            // Sort result in 'admins' and 'users'
            List<UserViewModel> users = new List<UserViewModel>();
            List<UserViewModel> admins = new List<UserViewModel>();
            foreach (var user in allUsers)
            {
                if (user.Role == "Admin")
                    admins.Add(user);
                else
                    users.Add(user);
            }

            ViewBag.Users = users;
            ViewBag.Admins = admins;
            return View();
        }
    }
}
