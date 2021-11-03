using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_CCSB.Models
{
    public class Rate
    {
        [Key]
        public string VehicleType { get; set; } // Camper or Caravan

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}