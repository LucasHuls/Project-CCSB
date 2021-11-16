﻿using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Services;
using System;

namespace Project_CCSB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleApiController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleApiController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Function for adding vehicles. It checks the commonresponse and will send the connected message from the class helper.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>CommonResponse</returns>
        [HttpPost]
        [Route("SaveVehicle")]
        public IActionResult SaveVehicle(VehicleViewModel data)
        {
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
            return Ok(commonResponse);
        }

        /// <summary>
        /// Function for removing vehicles. It checks the commonresponse and will send the connected message from the class helper.
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns>CommonResponse</returns>
        [HttpDelete]
        [Route("DeleteVehicle")]
        public IActionResult DeleteVehicle([FromHeader]string licensePlate)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.Status = _vehicleService.DeleteVehicle(licensePlate).Result;
                if (commonResponse.Status == 1)
                {
                    // Delete vehicle success
                    commonResponse.Message = "Voertuig is verwijderd";
                }
            }
            catch (Exception ex)
            {
                commonResponse.Message = ex.Message;
                commonResponse.Status = 0;
            }
            return Ok(commonResponse);
        }
    }
}
