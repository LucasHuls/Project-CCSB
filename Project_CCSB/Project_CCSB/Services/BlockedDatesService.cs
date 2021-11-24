using Microsoft.EntityFrameworkCore;
using Project_CCSB.Models;
using Project_CCSB.Models.ViewModels;
using Project_CCSB.Models.ViewModels.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Services
{
    public class BlockedDatesService : IBlockedDatesService
    {
        private readonly ApplicationDbContext _db;

        public BlockedDatesService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<BlockedDates> GetBlockedDates()
        {
            var blockeddates = _db.BlockedDates.ToList();

            return blockeddates;
        }

        public async Task<int> StoreDate(BlockedDatesViewModel model)
        {
            if (model != null && _db.BlockedDates.Any(x => x.SelectedDateToBeBlocked == model.SelectedDateToBeBlocked))
            {
                // date already exists
                return 1;
            }
            else
            {
                // Create new date
                BlockedDates date = new BlockedDates
                {
                    SelectedDateToBeBlocked = model.SelectedDateToBeBlocked
                };
                _db.BlockedDates.Add(date);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public async Task<int> DeleteBlockedDate(DateTime date)
        {
            BlockedDates BlockedDateToDelete = new BlockedDates
            {
                SelectedDateToBeBlocked = date
            };
            _db.BlockedDates.Remove(BlockedDateToDelete);
            await _db.SaveChangesAsync();
            return 1;
        }
    }
}
