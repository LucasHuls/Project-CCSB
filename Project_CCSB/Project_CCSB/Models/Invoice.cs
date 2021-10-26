using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_CCSB.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
    }
}
