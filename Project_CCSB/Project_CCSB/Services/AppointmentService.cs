using Microsoft.EntityFrameworkCore;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(AppointmentViewModel model)
        {
            // Check for valid appointment type
            if ((model.AppointmentType != "Brengen") && (model.AppointmentType != "Ophalen"))
            {
                return -1;
            }

            // Check for valid date
            if (model.Date < DateTime.Now)
            {
                return -1;
            }

            if (model != null && _db.Appointments.Any(x => x.LicensePlate == model.LicensePlate) && _db.Appointments.Any(x => x.Date == model.Date))
            {
                Appointment appointmentToDelete = new Appointment
                {
                    Date = model.Date,
                    LicensePlate = model.LicensePlate,
                    AppointmentType = model.AppointmentType
                };
                _db.Appointments.Remove(appointmentToDelete);
                await _db.SaveChangesAsync();
                return 1;
            }
            else
            {
                //Create appointment based on viewmodel
                Appointment appointment = new Appointment()
                {
                    Date = model.Date,
                    LicensePlate = model.LicensePlate,
                    AppointmentType = model.AppointmentType
                };
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public List<AppointmentViewModel> GetAppointments()
        {
            var appointmentlist = (from appointment in _db.Appointments
                                   join vehicle in _db.Vehicles on appointment.LicensePlate equals vehicle.LicensePlate
                                   select new AppointmentViewModel
                                   {
                                       Date = appointment.Date,
                                       AppointmentType = appointment.AppointmentType,
                                       LicensePlate = appointment.LicensePlate,
                                       ApplicationUserFullName = vehicle.ApplicationUser.FullName
                                   }).ToList();
            return appointmentlist;
        }

        public string GetUserByLicensePlate(string licensePlate)
        {
            var userName = (from vehicle in _db.Vehicles.Where(x => x.LicensePlate == licensePlate)
                            join user in _db.Users on vehicle.ApplicationUser.Id equals user.Id
                            select new ApplicationUser
                            {
                                FirstName = user.FirstName,
                                MiddleName = user.MiddleName,
                                LastName = user.LastName
                            }).FirstOrDefault();
            return userName.FullName;
        }

        public async Task<int> DeleteAppointment(DateTime date)
        {
            var appointment = _db.Appointments.Where(x => x.Date == date).AsNoTracking().ToList();

            Appointment appointmentToDelete = new Appointment
            {
                Date = appointment[0].Date,
                AppointmentType = appointment[0].AppointmentType,
                LicensePlate = appointment[0].LicensePlate
            };
            _db.Appointments.Remove(appointmentToDelete);
            await _db.SaveChangesAsync();
            return 1;
        }
    }
}
