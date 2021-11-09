using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project_CCSB.Models.ViewModels
{
    public class VehicleViewModel
    {
        [DisplayName("Kenteken")]
        [Required]
        public string LicensePlate { get; set; }

        [DisplayName("Type")]
        [Required]
        public string Type { get; set; }

        [DisplayName("Merk")]
        [Required]
        public string Brand { get; set; }

        [DisplayName("Lengte")]
        [Required]
        public string Length { get; set; }

        [DisplayName("Stroomaansluiting")]
        [Required]
        public string Power { get; set; }

        [DisplayName("Gebruiker")]
        [Required]
        public string User { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(MiddleName))
                    return FirstName + " " + LastName;
                else
                    return FirstName + " " + MiddleName + " " + LastName;
            }
        }
        public string PowerText
        {
            get
            {
                if (Power == "True")
                    return "Ja";
                else
                    return "Nee";
            }
        }
    }
}
