using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Models.ViewModels.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext _db;

        public VehicleService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<VehicleViewModel> GetVehicleList()
        {
            var vehicles = (from vehicle in _db.Vehicles
                            select new VehicleViewModel
                            {
                                LicensePlate = vehicle.LicensePlate,
                                Brand = vehicle.Brand,
                                Length = vehicle.Length,
                                Power = vehicle.Power,
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
    }
}
