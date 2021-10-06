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
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dd/MM/yyy}")]
        public DateTime AppointmentDate { get; set; }

        [DisplayName("Tijd:")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:HH:MM}")]
        public DateTime AppointmentTime { get; set; }

        [DisplayName("Type:")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string AppointmentType { get; set; }

        [DisplayName("Voertuig:")]
        public string AppointmentVehicle { get; set; }
    }
}
