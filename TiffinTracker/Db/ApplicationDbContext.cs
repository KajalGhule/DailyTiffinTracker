using Microsoft.EntityFrameworkCore;
using TiffinTracker.Models;

namespace TiffinTracker.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        public DbSet<Student> Student { get; set; }
        public DbSet<MealDistribution> MealDistribution { get; set; }
        public DbSet<MealAuditLog> MealAuditLog { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Define relationships if needed
        //    modelBuilder.Entity<MealDistribution>()
        //        .HasOne(m => m.Student)
        //        .WithMany(s => s.MealDistributions)
        //        .HasForeignKey(m => m.StudentId);
        //}
    }
}
