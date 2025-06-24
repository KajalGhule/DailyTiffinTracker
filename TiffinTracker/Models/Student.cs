using System.ComponentModel.DataAnnotations;

namespace TiffinTracker.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? UserId { get; set; }

        public bool IsActive { get; set; } = true;

        public User User { get; set; } // Navigation

        public ICollection<MealDistribution> MealDistributions { get; set; }

        public ICollection<MealAuditLog> MealAuditLogs { get; set; }
    }

}