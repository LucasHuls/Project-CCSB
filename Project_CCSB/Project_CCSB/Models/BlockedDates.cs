using System;
using System.ComponentModel.DataAnnotations;

namespace Project_CCSB.Models.ViewModels
{
    public class BlockedDates
    {
        [Key]
        public DateTime SelectedDateToBeBlocked { get; set; }
    }
}
