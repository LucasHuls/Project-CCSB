using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models.ViewModels
{
    public class VehicleViewModel
    {
        [DisplayName("Kenteken")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string LicensePlate { get; set; }

        [DisplayName("Soort voertuig")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Type { get; set; }

        [DisplayName("Merk")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Brand { get; set; }

        [DisplayName("Lengte")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public int Length { get; set; }

        [DisplayName("Stroomaansluiting")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public bool Power { get; set; }
    }
}
