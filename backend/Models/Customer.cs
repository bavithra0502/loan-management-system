using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoanManagementAPI.Models
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = string.Empty;

        [MaxLength(10)]
        public string Gender { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        [MaxLength(15)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(20)]
        public string AadhaarNumber { get; set; } = string.Empty;

        [MaxLength(20)]
        public string PANNumber { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Occupation { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal AnnualIncome { get; set; }

        public ICollection<LoanRequest>? LoanRequests { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }

        
    }
}
