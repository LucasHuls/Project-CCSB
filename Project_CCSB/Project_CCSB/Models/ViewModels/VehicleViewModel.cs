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

        [DisplayName("Lengte (in m)")]
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

        public bool IsDeleteAble { get; set; }

        [DisplayName("Kleur")]
        [Required]
        public string Color { get; set; }

        [DisplayName("Bouwjaar")]
        [Required]
        public int BuildYear { get; set; }

        [DisplayName("Aantal slaapplaatsen")]
        [Required]
        public int NumberOfBeds { get; set; }

        [DisplayName("Fietsdrager")]
        [Required]
        public bool BicycleCarrier { get; set; }

        [DisplayName("Airco")]
        [Required]
        public bool Airconditioning { get; set; }

        // Extra properties for camper's
        [DisplayName("Kilometerstand")]
        public int Mileage { get; set; }

        [DisplayName("Aantal pk's")]
        public int HorsePower { get; set; }

        [DisplayName("Camper type")]
        public string CamperType { get; set; }

        [DisplayName("Trekhaak")]
        public bool TowBar { get; set; }

        // Extra properties for caravan's
        [DisplayName("Ledig gewicht")]
        public int EmptyWeight { get; set; }

        [DisplayName("Vuilwatertank")]
        public bool HoldingTank { get; set; }
    }
}
