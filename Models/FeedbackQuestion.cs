using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("FeedbackQuestion")]
    public class FeedbackQuestion
    {
        [Key]
        public int QuestionId { get; set; }

        [Required, MaxLength(300)]
        public string Question { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        
    }
}
