using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models.ViewModels.Vehicle
{
    public class VehicleViewModel
    {
        public string LicensePlate { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public decimal Length { get; set; }

        public bool Power { get; set; }

        public string PowerText
        {
            get
            {
                if (Power)
                {
                    return "Ja";
                }
                else
                {
                    return "Nee";
                }
            }
        }
    }
}
