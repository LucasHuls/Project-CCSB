using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_CCSB.Models;
using Project_CCSB.Services;
using System.Collections.Generic;
using System.Linq;

namespace Project_CCSB.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly ApplicationDbContext _db;
        private readonly IContractService _contractService;

        public AppointmentController(IVehicleService vehicleService, ApplicationDbContext db, IContractService contractService)
        {
            _vehicleService = vehicleService;
            _db = db;
            _contractService = contractService;
        }

        [Authorize]
        public IActionResult Index()
        {
            // Check if contract needs to be renewed
            _contractService.CheckContract();

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
