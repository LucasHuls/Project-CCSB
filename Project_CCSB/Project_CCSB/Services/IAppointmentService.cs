using Project_CCSB.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Project_CCSB.Services
{
    public interface IAppointmentService
    {
        /// <summary>
        /// Adds new appointments to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddUpdate(AppointmentViewModel model);

        /// <summary>
        /// Gets all appointments from the database
        /// </summary>
        /// <returns>List of AppointmentViewModels</returns>
        public List<AppointmentViewModel> GetAppointments();

        /// <summary>
        /// Gets appointments from specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of AppointmentViewModels</returns>
        public List<AppointmentViewModel> GetUserAppointments(string userId);

        /// <summary>
        /// Gets Applicationuser using vehicle licenseplate
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns>Applicationusers full name</returns>
        public string GetUserByLicensePlate(string licensePlate);

        /// <summary>
        /// Deletes an appointment from the database
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public Task<int> DeleteAppointment(DateTime date);
    }
}
