using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Project_CCSB.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ApplicationDbContext _db;

        public AppointmentController(IVehicleService vehicleService, ApplicationDbContext db)
        {
            _vehicleService = vehicleService;
            _db = db;
        }

        public IActionResult Index()
        {
            ViewBag.Vehicles = new SelectList(_vehicleService.GetVehicleList(), "LicensePlate", "LicensePlate", "", "Brand");

            ViewBag.Users = new SelectList(_vehicleService.GetUserList(), "Name", "Name");
            return View();
        }

        private string GetUserName(string licensePlate)
        {
            var name = (from vehicle in _db.Vehicles.Where(x => x.LicensePlate == licensePlate)
                            join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                            select new ApplicationUser
                            {
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName
                            }).FirstOrDefault();
            return name.FullName;
        }
    }
}
