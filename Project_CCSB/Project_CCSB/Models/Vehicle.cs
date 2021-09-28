using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models
{
    public class Vehicle
    {
        public ApplicationUser ApplicationUser { get; set; }

        [Key]
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int Length { get; set; }
        public bool Power { get; set; }
    }
}
