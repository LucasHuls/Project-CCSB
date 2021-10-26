using System;
using System.ComponentModel.DataAnnotations;

namespace Project_CCSB.Models
{
    public class Contract
    {
        [Key]
        public int ContractID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Vehicle Vehicle { get; set; }
        public Invoice Invoice { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
