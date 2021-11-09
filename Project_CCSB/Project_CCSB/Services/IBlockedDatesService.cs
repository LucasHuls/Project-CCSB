using Project_CCSB.Models.ViewModels;
using Project_CCSB.Models.ViewModels.Appointment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public interface IBlockedDatesService
    {
        public List<BlockedDates> GetBlockedDates();
        public Task<int> StoreDate(BlockedDatesViewModel model);
    }
}
