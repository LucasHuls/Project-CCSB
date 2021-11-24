using Project_CCSB.Models.ViewModels;
using Project_CCSB.Models.ViewModels.Appointment;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IBlockedDatesService
    {
        /// <summary>
        /// Gets all blockeddates from the database
        /// </summary>
        /// <returns>List of BlockedDates</returns>
        public List<BlockedDates> GetBlockedDates();

        /// <summary>
        /// Adds a new Blocked date to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Interger based on result</returns>
        public Task<int> StoreDate(BlockedDatesViewModel model);

        /// <summary>
        /// Removes a Blocked date from the database
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Interger based on result</returns>
        public Task<int> DeleteBlockedDate(DateTime date);
    }
}
