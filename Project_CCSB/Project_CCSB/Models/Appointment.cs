﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models
{
    public class Appointment
    {
        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }

        [Key, Column(Order = 2)]
        public string LicensePlate { get; set; }

        public string AppointmentType { get; set; }
    }
}
