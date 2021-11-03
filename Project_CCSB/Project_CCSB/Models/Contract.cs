using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_CCSB.Models
{
    public class Contract
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Vehicle Vehicle { get; set; }
        public Invoice Invoice { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
