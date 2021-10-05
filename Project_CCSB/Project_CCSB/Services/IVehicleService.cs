using Project_CCSB.Models.ViewModels;
using Project_CCSB.Models.ViewModels.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IVehicleService
    {
        public List<VehicleViewModel> GetVehicleList();
        public List<UserViewModel> GetUserList();
    }
}
