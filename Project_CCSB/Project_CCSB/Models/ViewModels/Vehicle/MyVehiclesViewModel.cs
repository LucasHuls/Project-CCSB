using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_CCSB.Models.ViewModels
{
    public class MyVehiclesViewModel
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
                } else
                {
                    return "Nee";
                }
            }
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                if (MiddleName == "")
                {
                    return FirstName + " " + LastName;
                } else
                {
                    return FirstName + " " + MiddleName + " " + LastName;
                }
            }
        }
    }
}
