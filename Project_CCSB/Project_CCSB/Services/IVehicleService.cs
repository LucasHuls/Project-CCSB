using Project_CCSB.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IVehicleService
    {
        /// <summary>
        /// Gets vehicles of user
        /// </summary>
        /// <returns>List of VehicleViewModels</returns>
        public List<VehicleViewModel> GetVehicleList();

        /// <summary>
        /// Gets a list of users with the "User" role
        /// </summary>
        /// <returns>List of UserViewModels</returns>
        public List<UserViewModel> GetUserList();

        /// <summary>
        /// Gets list of vehicle based on user
        /// </summary>
        /// <returns>List of VehicleViewModels</returns>
        public List<VehicleViewModel> GetUserVehicleList();

        /// <summary>
        /// Adds a new vehicle to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Interger based on result</returns>
        public Task<int> StoreVehicle(VehicleViewModel model);

        /// <summary>
        /// Deletes vehicle from database
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns>Interger based on result</returns>
        public Task<int> DeleteVehicle(string licensePlate);
    }
}
