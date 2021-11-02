using System;

namespace Project_CCSB.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public DateTime Date { get; set; }

        public string LicensePlate { get; set; }

        public string AppointmentType { get; set; }

        public string ApplicationUserFullName { get; set; }
    }
}
