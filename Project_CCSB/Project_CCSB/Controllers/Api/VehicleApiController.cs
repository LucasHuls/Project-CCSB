using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_CCSB.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleApiController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        private readonly HttpContextAccessor _httpContextAccessor;

        public VehicleApiController(IVehicleService vehicleService, HttpContextAccessor httpContextAccessor)
        {
            _vehicleService = vehicleService;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
