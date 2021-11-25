using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Services;
using System;

namespace Project_CCSB.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// function for adding vehicles
        /// </summary>
        /// <returns>view</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult AddVehicle()
        {
            ViewBag.UserList = new SelectList(_vehicleService.GetUserList(), "Id", "Name");
            return View();
        }

        /// <summary>
        /// Return users vehicles
        /// </summary>
        /// <returns>View</returns>
        public IActionResult UserVehicles()
        {
            var vehicles = _vehicleService.GetUserVehicleList();
            ViewBag.Vehicles = vehicles;
            return View();
        }

        /// <summary>
        /// Returns all existing vehicles
        /// </summary>
        /// <returns>View</returns>
        [Authorize(Roles = "Admin")]
        public IActionResult AllVehicles()
        {
            var vehicles = _vehicleService.GetVehicleList();
            ViewBag.Vehicles = vehicles;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult AddVehicle(VehicleViewModel model)
        {
            if (model.Type == "Camper")
            {
                CommonResponse<int> commonResponse = AddCamper(model);
                if (commonResponse.Status == 0)
                {
                    ModelState.AddModelError("", "Er is iets fout gegaan");
                }
            }
            else if (model.Type == "Caravan")
            {
                // AddCaravan(model);
            }

            return View();
        }

        public CommonResponse<int> AddCamper(VehicleViewModel model)
        {
            VehicleViewModel data = new VehicleViewModel()
            {
                LicensePlate = model.LicensePlate,
                Type = model.Type,
                Brand = model.Brand,
                Length = model.Length,
                Power = model.Power,
                User = model.User,
                Color = model.Color,
                BuildYear = model.BuildYear,
                NumberOfBeds = model.NumberOfBeds,
                BicycleCarrier = model.BicycleCarrier,
                Airconditioning = model.Airconditioning,

                Mileage = model.Mileage,
                HorsePower = model.HorsePower,
                CamperType = model.CamperType,
                TowBar = model.TowBar,

                EmptyWeight = 0,
                HoldingTank = false
            };

            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _vehicleService.StoreVehicle(data).Result;
                if (commonResponse.Status == 1)
                {
                    // Update vehicle success
                    commonResponse.Message = "Voertuig is aangepast";
                }
                else if (commonResponse.Status == 2)
                {
                    // Succesful added vehicle
                    commonResponse.Message = "Voertuig is toegevoegd";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 0;
            }

            return commonResponse;
        }
    }
}
