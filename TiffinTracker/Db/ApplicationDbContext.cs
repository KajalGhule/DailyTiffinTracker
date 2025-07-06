using Microsoft.EntityFrameworkCore;
using TiffinTracker.Models;

namespace TiffinTracker.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

        public DbSet<User> User { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<MealDistribution> MealDistribution { get; set; }
        public DbSet<MealAuditLog> MealAuditLog { get; set; }

        public DbSet<ProviderStudent> ProviderStudent { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MealDistribution>()
                .Property(m => m.MealType)
                .HasConversion(
                    v => v.ToString(), // enum → string for MySQL
                    v => (MealType)Enum.Parse(typeof(MealType), v) // string → enum for C#
                );

            base.OnModelCreating(modelBuilder);

           
        }


    }
}
