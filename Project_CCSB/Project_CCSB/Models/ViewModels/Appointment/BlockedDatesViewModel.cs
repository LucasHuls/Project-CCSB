using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_CCSB.Models.ViewModels.Appointment
{
    public class BlockedDatesViewModel
    {
        [DisplayName("Datum")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.Date)]
        public DateTime SelectedDateToBeBlocked { get; set; }
    }
}
