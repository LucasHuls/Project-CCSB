using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_CCSB.Models
{
    public class BlockedDate
    {
        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }

        [Key, Column(Order = 2)]
        public string LicensePlate { get; set; }

        public string AppointmentType { get; set; }
    }
}
