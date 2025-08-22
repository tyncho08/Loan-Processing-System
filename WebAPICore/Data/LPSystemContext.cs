using Microsoft.EntityFrameworkCore;
using LPSystemWebAPICore.Models;

namespace LPSystemWebAPICore.Data
{
    public class LPSystemContext : DbContext
    {
        public LPSystemContext(DbContextOptions<LPSystemContext> options) : base(options)
        {
        }

        public DbSet<UserTable> UserTables { get; set; }
        public DbSet<LoanTable> LoanTables { get; set; }
        public DbSet<ApplTable> ApplTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key for ApplTable
            modelBuilder.Entity<ApplTable>()
                .HasKey(a => new { a.AppId, a.UserId, a.LoanId });

            // Configure relationships
            modelBuilder.Entity<ApplTable>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ApplTable>()
                .HasOne(a => a.Loan)
                .WithMany()
                .HasForeignKey(a => a.LoanId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data
            SeedData(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Users
            modelBuilder.Entity<UserTable>().HasData(
                new UserTable { UserId = 1, UserName = "admin", UserGender = "Male", UserEmail = "admin@lpsystem.com", UserPass = "admin123", UserRole = "Admin" },
                new UserTable { UserId = 2, UserName = "john_doe", UserGender = "Male", UserEmail = "john@example.com", UserPass = "password123", UserRole = "Customer" },
                new UserTable { UserId = 3, UserName = "jane_smith", UserGender = "Female", UserEmail = "jane@example.com", UserPass = "password123", UserRole = "Customer" },
                new UserTable { UserId = 4, UserName = "client1", UserGender = "Male", UserEmail = "client1@lpsystem.com", UserPass = "client123", UserRole = "Client" }
            );

            // Seed Loans
            modelBuilder.Entity<LoanTable>().HasData(
                new LoanTable { LoanId = 1, LoanType = "Personal Loan", LoanAmt = 50000, LoanROI = 12.5m, LoanPeriod = 12 },
                new LoanTable { LoanId = 2, LoanType = "Home Loan", LoanAmt = 500000, LoanROI = 8.5m, LoanPeriod = 240 },
                new LoanTable { LoanId = 3, LoanType = "Car Loan", LoanAmt = 300000, LoanROI = 9.5m, LoanPeriod = 60 },
                new LoanTable { LoanId = 4, LoanType = "Education Loan", LoanAmt = 200000, LoanROI = 10.5m, LoanPeriod = 48 }
            );

            // Seed Applications
            modelBuilder.Entity<ApplTable>().HasData(
                new ApplTable { AppId = 1, UserId = 2, LoanId = 1, UserMob = "9876543210", UserAdhaar = "123456789012", LoanAmt = 50000, AppStatus = "Pending" },
                new ApplTable { AppId = 2, UserId = 3, LoanId = 2, UserMob = "9876543211", UserAdhaar = "123456789013", LoanAmt = 500000, AppStatus = "Approved" }
            );
        }
    }
}