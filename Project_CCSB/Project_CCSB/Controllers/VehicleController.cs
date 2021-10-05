using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers
{
    [Authorize]
    public class VehicleController : Controller
    {
        private readonly ApplicationDbContext _db;
        UserManager<ApplicationUser> _userManager;

        private readonly IVehicleService _vehicleService;

        public VehicleController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager, IVehicleService vehicleService)
        {
            _db = db;
            _userManager = userManager;
            _vehicleService = vehicleService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddVehicle()
        {

            ViewBag.UserSelect = new SelectList(_vehicleService.GetUserList(), "Id", "Name");
            return View();
        }

        /// <summary>
        /// Adds new vehicle to database
        /// </summary>
        /// <param name="model"></param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddVehicle(AddVehicleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.User);

            if (ModelState.IsValid)
            {
                // First check if license plate is not a duplicate
                var duplicate = _db.Vehicles.Any(x => x.LicensePlate == model.LicensePlate);
                if (duplicate)
                {
                    ModelState.AddModelError("", "Kenteken bestaat al");
                    ViewBag.UserSelect = new SelectList(_vehicleService.GetUserList(), "Id", "Name");
                    return View();
                }

                // Check if input length is decimal value
                if (!decimal.TryParse(model.Length, out _))
                {
                    ModelState.AddModelError("", "Geen geldige lengte");
                    ViewBag.UserSelect = new SelectList(_vehicleService.GetUserList(), "Id", "Name");
                    return View();
                }


                Vehicle newVehicle = new Vehicle()
                {
                    LicensePlate = model.LicensePlate,
                    Type = model.Type,
                    Brand = model.Brand,
                    Length = decimal.Parse(model.Length.Replace('.', ',')),
                    Power = model.Power,
                    ApplicationUser = user
                };
                _db.Vehicles.Add(newVehicle);
                await _db.SaveChangesAsync();
                return RedirectToAction("AllVehicles", "Vehicle");
            }
            return View();
        }

        /// <summary>
        /// Show all the users vehicles
        /// </summary>
        public async Task<IActionResult> MyVehicles()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var vehicles = (from vehicle in _db.Vehicles
                            join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                            where vehicle.ApplicationUser == currentUser
                            select new MyVehiclesViewModel
                            { 
                                LicensePlate = vehicle.LicensePlate,
                                Type = vehicle.Type,
                                Brand = vehicle.Brand,
                                Length = vehicle.Length,
                                Power = vehicle.Power,

                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName
                            }).ToList();
            ViewBag.Vehicles = vehicles;
            return View();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteVehicle(string id)
        {
            Console.WriteLine(id);
            return RedirectToAction("AllVehicles", "Vehicle");
        }

        /// <summary>
        /// Show all the registered vehicles, only access with Admin role.
        /// </summary>
        [Authorize(Roles = "Admin")]
        public IActionResult AllVehicles()
        {
            var vehicles = (from vehicle in _db.Vehicles
                            join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                            select new MyVehiclesViewModel
                            {
                                LicensePlate = vehicle.LicensePlate,
                                Type = vehicle.Type,
                                Brand = vehicle.Brand,
                                Length = vehicle.Length,
                                Power = vehicle.Power,

                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName
                            }).OrderBy(x => x.LastName).ToList();
            ViewBag.Vehicles = vehicles;
            return View();
        }
    }
}
