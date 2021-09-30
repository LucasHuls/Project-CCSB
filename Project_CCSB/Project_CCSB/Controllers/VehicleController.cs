﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
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

        public VehicleController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddVehicle()
        {
            // Get all users with role of 'user'
            var users = (from user in _db.Users
                         join userRole in _db.UserRoles on user.Id equals userRole.UserId
                         join role in _db.Roles.Where(x => x.Name == Helper.User) on userRole.RoleId equals role.Id
                         select new UserViewModel
                         { 
                             Id = user.Id,
                             Name = string.IsNullOrEmpty(user.MiddleName) ? 
                                user.FirstName + " " + user.LastName :
                                user.FirstName + " " + user.MiddleName + " " + user.LastName
                         }).OrderBy(u => u.Name).ToList();

            SelectList items = new SelectList(users, "Id", "Name");
            ViewBag.UserSelect = items;
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
                Vehicle newVehicle = new Vehicle()
                {
                    LicensePlate = model.LicensePlate,
                    Type = model.Type,
                    Brand = model.Brand,
                    Length = model.Length,
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
