using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("LoanRequest")]
    public class LoanRequest
    {
        [Key]
        public int LoanRequestId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Required, MaxLength(50)]
        public string LoanType { get; set; } = string.Empty; // Home, Car, Education

        [Column(TypeName = "decimal(18,2)")]
        public decimal LoanAmount { get; set; }

        public int LoanPeriod { get; set; } // months

        [MaxLength(250)]
        public string Purpose { get; set; } = string.Empty;

        public DateTime ApplyDate { get; set; } = DateTime.Now;

        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; // Pending / Approved / Rejected

        public BackgroundVerification? BackgroundVerification { get; set; }
        public LoanVerification? LoanVerification { get; set; }
    }
}
