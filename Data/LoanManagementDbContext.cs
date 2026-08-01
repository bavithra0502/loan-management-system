using LoanManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanManagementAPI.Data
{
    public class LoanManagementDbContext : DbContext
    {
        public LoanManagementDbContext(DbContextOptions<LoanManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LoanOfficer> LoanOfficers { get; set; }
        public DbSet<LoanRequest> LoanRequests { get; set; }
        public DbSet<BackgroundVerification> BackgroundVerifications { get; set; }
        public DbSet<LoanVerification> LoanVerifications { get; set; }
        public DbSet<HelpReport> HelpReports { get; set; }
        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Users -> Customer (1:1)
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Users -> LoanOfficer (1:1)
            modelBuilder.Entity<LoanOfficer>()
                .HasOne(o => o.User)
                .WithOne(u => u.LoanOfficer)
                .HasForeignKey<LoanOfficer>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer -> LoanRequest (1:M)
            modelBuilder.Entity<LoanRequest>()
                .HasOne(lr => lr.Customer)
                .WithMany(c => c.LoanRequests)
                .HasForeignKey(lr => lr.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // LoanRequest -> BackgroundVerification (1:1)
            modelBuilder.Entity<BackgroundVerification>()
                .HasOne(bv => bv.LoanRequest)
                .WithOne(lr => lr.BackgroundVerification)
                .HasForeignKey<BackgroundVerification>(bv => bv.LoanRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BackgroundVerification>()
                .HasOne(bv => bv.Officer)
                .WithMany()
                .HasForeignKey(bv => bv.OfficerId)
                .OnDelete(DeleteBehavior.Restrict);

            // LoanRequest -> LoanVerification (1:1)
            modelBuilder.Entity<LoanVerification>()
                .HasOne(lv => lv.LoanRequest)
                .WithOne(lr => lr.LoanVerification)
                .HasForeignKey<LoanVerification>(lv => lv.LoanRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LoanVerification>()
                .HasOne(lv => lv.Officer)
                .WithMany()
                .HasForeignKey(lv => lv.OfficerId)
                .OnDelete(DeleteBehavior.Restrict);

            // HelpReport -> User
            modelBuilder.Entity<HelpReport>()
                .HasOne(h => h.User)
                .WithMany()
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Feedback -> Customer / FeedbackQuestion
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Customer)
                .WithMany(c => c.Feedbacks)
                .HasForeignKey(f => f.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Question)
                .WithMany()
                .HasForeignKey(f => f.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraints
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        }
    }
}
