using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        // Admin / Customer / LoanOfficer
        [Required, MaxLength(20)]
        public string Role { get; set; } = string.Empty;

        // Pending / Approved / Rejected
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation
        public Customer? Customer { get; set; }
        public LoanOfficer? LoanOfficer { get; set; }
    }
}
