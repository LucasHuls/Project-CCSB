using System.ComponentModel.DataAnnotations;

namespace Project_CCSB.Models
{
    public class Vehicle
    {
        public ApplicationUser ApplicationUser { get; set; }

        [Key]
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public decimal Length { get; set; }
        public bool Power { get; set; }
    }
}
