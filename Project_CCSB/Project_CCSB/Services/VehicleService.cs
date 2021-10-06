using Microsoft.AspNetCore.Identity;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public VehicleService(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public List<VehicleViewModel> GetVehicleList()
        {
            var vehicles = (from vehicle in _db.Vehicles
                            select new VehicleViewModel 
                            {
                                LicensePlate = vehicle.LicensePlate,
                                Brand = vehicle.Brand,
                                Power = vehicle.Power.ToString(),
                                Type = vehicle.Type
                            }).OrderBy(x => x.Brand).ToList();

            return vehicles;
        }

        public List<UserViewModel> GetUserList()
        {
            var users = (from user in _db.Users
                         join userRole in _db.UserRoles on user.Id equals userRole.UserId
                         join role in _db.Roles.Where(x => x.Name == Helper.User) on userRole.RoleId equals role.Id
                         select new UserViewModel
                         {
                             Id = user.Id,
                             Name = string.IsNullOrEmpty(user.MiddleName) ?
                             user.FirstName + " " + user.LastName :
                             user.FirstName + " " + user.MiddleName + " " + user.LastName
                         }).OrderBy(u => u.Name).ToList();

            return users;
        }

        public async Task<int> AddUpdate(VehicleViewModel model)
        {
            // Get User
            var user = await _userManager.FindByIdAsync(model.User);
            decimal length = Decimal.Parse(model.Length);
            bool power = model.Power == "true" ? true : false;

            if (model != null && _db.Vehicles.Any(x => x.LicensePlate == model.LicensePlate))
            {
                // Add code for update
                return 1;
            }
            else
            {
                // Create new vehicle
                Vehicle vehicle = new Vehicle
                {
                    ApplicationUser = user,
                    LicensePlate = model.LicensePlate,
                    Brand = model.Brand,
                    Length = length,
                    Power = power,
                    Type = model.Type
                };
                _db.Vehicles.Add(vehicle);
                await _db.SaveChangesAsync();
                return 2;
            }
        }
    }
}
