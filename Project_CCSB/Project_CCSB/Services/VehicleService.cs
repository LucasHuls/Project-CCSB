using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string _userId;

        public VehicleService(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _userManager = userManager;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public List<VehicleViewModel> GetVehicleList()
        {
            var vehicles = (from vehicle in _db.Vehicles
                            join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                            select new VehicleViewModel 
                            {
                                LicensePlate = vehicle.LicensePlate,
                                Brand = vehicle.Brand,
                                Power = vehicle.Power.ToString(),
                                Length = vehicle.Length.ToString(),
                                Type = vehicle.Type,
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName
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
                             FirstName = user.FirstName,
                             MiddleName = user.MiddleName,
                             LastName = user.LastName
                         }).OrderBy(u => u.LastName).ToList();

            return users;
        }

        public List<VehicleViewModel> GetUserVehicleList()
        {
            var vehicles = (from vehicle in _db.Vehicles
                            join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                            where user.Id == _userId
                            select new VehicleViewModel
                            {
                                LicensePlate = vehicle.LicensePlate,
                                Brand = vehicle.Brand,
                                Power = vehicle.Power.ToString(),
                                Length = vehicle.Length.ToString(),
                                Type = vehicle.Type,
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName
                            }).OrderBy(x => x.Brand).ToList();

            return vehicles;
        }

        public async Task<int> StoreVehicle(VehicleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.User);    // Get User
            decimal length = Decimal.Parse(model.Length);               // Convert string to correct type (decimal)
            bool power = model.Power == "true" ? true : false;          // Turn string into correct type (boolean)

            if (model != null && _db.Vehicles.Any(x => x.LicensePlate == model.LicensePlate))
            {
                // Vehicle already exists
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

        public async Task<int> DeleteVehicle(string licensePlate)
        {
            var vehicle = _db.Vehicles.Where(x => x.LicensePlate == licensePlate).AsNoTracking().ToList();

            Vehicle vehicleToDelete = new Vehicle 
            { 
                ApplicationUser = vehicle[0].ApplicationUser,
                LicensePlate = vehicle[0].LicensePlate,
                Brand = vehicle[0].Brand,
                Length = vehicle[0].Length,
                Power = vehicle[0].Power,
                Type = vehicle[0].Type
            };
            _db.Vehicles.Remove(vehicleToDelete);
            await _db.SaveChangesAsync();
            return 1;
        }
    }
}
