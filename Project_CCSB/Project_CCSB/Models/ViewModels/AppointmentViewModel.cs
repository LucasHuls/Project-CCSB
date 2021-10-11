using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models.ViewModels
{
    public class AppointmentViewModel
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string LicensePlate { get; set; }
        public bool AppointmentType { get; set; }
    }
}
