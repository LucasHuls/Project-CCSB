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
            if (model != null)
            {
                //TODO: Add code for update appointment
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
    }
}
