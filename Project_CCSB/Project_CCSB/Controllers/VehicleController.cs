using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_CCSB.Services;

namespace Project_CCSB.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddVehicle()
        {
            ViewBag.UserList = new SelectList(_vehicleService.GetUserList(), "Id", "Name");
            return View();
        }

        public IActionResult UserVehicles()
        {
            var vehicles = _vehicleService.GetUserVehicleList();
            ViewBag.Vehicles = vehicles;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllVehicles()
        {
            var vehicles = _vehicleService.GetVehicleList();
            ViewBag.Vehicles = vehicles;
            return View();
        }
    }
}
