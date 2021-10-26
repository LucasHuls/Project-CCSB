using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Project_CCSB.Services
{
    public interface IAppointmentService
    {
        public Task<int> AddUpdate(AppointmentViewModel model);
        public List<Appointment> GetAppointments();
        public Task<int> DeleteAppointment(DateTime date);
    }
}
