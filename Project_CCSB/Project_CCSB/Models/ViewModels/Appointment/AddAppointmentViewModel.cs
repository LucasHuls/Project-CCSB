using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models.ViewModels.Appointment
{
    public class AddAppointmentViewModel
    {
        [DisplayName("Datum:")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public DateTime AppointmentDate { get; set; }

        [DisplayName("Type:")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string AppointmentType { get; set; }

        [DisplayName("Voertuig:")]
        public string AppointmentVehicle { get; set; }

        [DisplayName("Gebruiker:")]
        public string AppointmentUser { get; set; }
    }
}
