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
    }
}
