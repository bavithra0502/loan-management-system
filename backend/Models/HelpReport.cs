using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("HelpReport")]
    public class HelpReport
    {
        [Key]
        public int HelpReportId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required, MaxLength(100)]
        public string Subject { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Reply { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Status { get; set; } = "Open"; // Open / Closed
    }
}
