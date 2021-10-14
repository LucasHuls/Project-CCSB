using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IAppointmentService
    {
        public Task<int> AddUpdate(AppointmentViewModel model);
        public List<Appointment> GetAppointments();
    }
}
