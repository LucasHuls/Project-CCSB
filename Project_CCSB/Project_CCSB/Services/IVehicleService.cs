using Project_CCSB.Models.ViewModels;
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
        public Task<int> AddUpdate(VehicleViewModel model);
        public Task<int> DeleteVehicle(string licensePlate);
    }
}
