using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailSender _emailSender;

        public AccountController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Login()
        {
            if (!_roleManager.RoleExistsAsync(Helper.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(Helper.Admin));
                await _roleManager.CreateAsync(new IdentityRole(Helper.User));
            }
            if (!_db.Users.Any()) // If no users exists create default admin
            {
                ApplicationUser defaultUser = new()
                {
                    UserName = "beheerder@gmail.com",
                    Email = "beheerder@gmail.com",
                    FirstName = "Beheerder",
                    MiddleName = null,
                    LastName = "Account",
                    AccountNumber = "123456789",
                    City = "Hengelo",
                    Adress = "Gieterij 200",
                    ZipCode = "1234DD",
                    BirthDate = DateTime.Now
                };
                var result = await _userManager.CreateAsync(defaultUser, "Beheerder!1");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
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

        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }

        private static bool ValidateAccountNumber(string accountNumber)
        {
            if (Regex.IsMatch(accountNumber, @"^([A-Z]{2}[ \-]?[0-9]{2})(?=(?:[ \-]?[A-Z0-9]){9,30}$)((?:[ \-]?[A-Z0-9]{3,5}){2,7})([ \-]?[A-Z0-9]{1,3})?$"))
            {
                return true;
            } else
                return false;
        }

        private static bool ValidateZipCode(string zipcode)
        {
            if (Regex.IsMatch(zipcode.ToUpper(), @"[1-9]\d{3}[A-Za-z]{2}")) //Check the zipcode with the following format:
            {
                return true;
            }
            else
                return false;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ValidateAccountNumber(model.AccountNumber))
                {
                    if (ValidateZipCode(model.ZipCode)) {
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

                            var messageToAdmin = new Message(new string[] { "projectCCSB@gmail.com" }, "Account geregistreerd", "Profiel bekijken", "accountRegistered");
                            var messageToUser = new Message(new string[] { model.Email }, "Account geregistreerd", "Profiel bekijken", "accountRegistered");
                            _emailSender.SendEmail(messageToAdmin);
                            _emailSender.SendEmail(messageToUser);
                            return RedirectToAction("AllUsers", "Account");
                        }
                        // Add all errors to the modelstate
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Postcode is niet geldig! Gebruik bijvoorveeld: 1234AB");
                    }
                } else
                {
                    ModelState.AddModelError("", "Geen valide rekening nummer");
                }
            }
            return View();
        }

        [Authorize]
        public IActionResult ChangePassWord()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> ChangePassWord(ChangePasswordViewModel model)
        {
            if (model.NewPassWord != model.NewPassWordConfirm)
            {
                ModelState.AddModelError("", "Wachtwoorden komen niet overeen");
                return View();
            }

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassWord, model.NewPassWord);

            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Oud wachtwoord is verkeerd");
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
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName,
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
        
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(new ApplicationUser {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AccountNumber = user.AccountNumber,
                City = user.City,
                Adress = user.Adress,
                ZipCode = user.ZipCode
            });
        }
    }
}
