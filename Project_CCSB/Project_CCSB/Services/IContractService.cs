using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IContractService
    {
        /// <summary>
        /// Check if an appointment is the first appointment
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns>True or False</returns>
        public bool IsFirstAppointment(string licensePlate);

        /// <summary>
        /// Creates a new contract in the database
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public Task MakeContract(AppointmentViewModel appointment);


        /// <summary>
        /// Changes price of vehicle
        /// </summary>
        /// <param name="model"></param>
        public void SetNewPrice(Rate model);

        /// <summary>
        /// Renews contracts every 31 of December
        /// </summary>
        public void RenewContracts();
    }
}
