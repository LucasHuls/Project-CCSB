using Project_CCSB.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IVehicleService
    {
        public List<VehicleViewModel> GetVehicleList();
        public List<UserViewModel> GetUserList();
        public List<VehicleViewModel> GetUserVehicleList();
        public Task<int> StoreVehicle(VehicleViewModel model);
        public Task<int> DeleteVehicle(string licensePlate);
    }
}
