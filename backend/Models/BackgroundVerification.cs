using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("BackgroundVerification")]
    public class BackgroundVerification
    {
        [Key]
        public int VerificationId { get; set; }

        [ForeignKey("LoanRequest")]
        public int LoanRequestId { get; set; }
        public LoanRequest? LoanRequest { get; set; }

        [ForeignKey("Officer")]
        public int OfficerId { get; set; }
        public LoanOfficer? Officer { get; set; }

        public DateTime VerificationDate { get; set; } = DateTime.Now;

        [MaxLength(300)]
        public string Remarks { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "Pending"; 
    }
}
