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
        public string LicensePlate { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("Merk")]
        public string Brand { get; set; }

        [DisplayName("Lengte")]
        public string Length { get; set; }

        [DisplayName("Stroomaansluiting")]
        public string Power { get; set; }

        [DisplayName("Gebruiker")]
        public string User { get; set; }
    }
}
