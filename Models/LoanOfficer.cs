using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("LoanOfficer")]
    public class LoanOfficer
    {
        [Key]
        public int OfficerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required, MaxLength(100)]
        public string OfficerName { get; set; } = string.Empty;

        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(20)]
        public string EmployeeCode { get; set; } = string.Empty;
    }
}
