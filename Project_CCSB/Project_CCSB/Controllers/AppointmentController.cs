using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_CCSB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public AppointmentController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public IActionResult Index()
        {
            ViewBag.Vehicles = new SelectList(_vehicleService.GetVehicleList(), "LicensePlate", "LicensePlate", "", "Brand");
            ViewBag.Users = new SelectList(_vehicleService.GetUserList(), "Name", "Name");
            return View();
        }
    }
}
