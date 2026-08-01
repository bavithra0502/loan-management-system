using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public FeedbackQuestion? Question { get; set; }

        [MaxLength(500)]
        public string Answer { get; set; } = string.Empty;

        public DateTime FeedbackDate { get; set; } = DateTime.Now;
    }
}
