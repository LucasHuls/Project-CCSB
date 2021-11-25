using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_CCSB.Models
{
    public class Vehicle
    {
        public ApplicationUser ApplicationUser { get; set; }

        [Key]
        public string LicensePlate { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Length { get; set; }

        public bool Power { get; set; }

        public string Color { get; set; }

        public int BuildYear { get; set; }

        public int NumberOfBeds { get; set; }

        public bool BicycleCarrier { get; set; }

        public bool Airconditioning { get; set; }

        // Extra properties for camper's
        public int Mileage { get; set; }

        public int HorsePower { get; set; }

        public string CamperType { get; set; }

        public bool TowBar { get; set; }

        // Extra properties for caravan's
        public int EmptyWeight { get; set; }

        public bool HoldingTank { get; set; }
    }
}
