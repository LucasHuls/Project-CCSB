using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers
{
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;

        public VehicleController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult AddVehicle()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVehicle(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Vehicle newVehicle = new Vehicle()
                {
                    LicensePlate = model.LicensePlate,
                    Type = model.Type,
                    Brand = model.Brand,
                    Length = model.Length,
                    Power = model.Power,
                };
                var result = await _db.Vehicles.AddAsync(newVehicle);
            }
            return View();
        }
    }
}
