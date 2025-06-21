using System.ComponentModel.DataAnnotations;

namespace TiffinTracker.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<MealDistribution> MealDistributions { get; set; }
    }
}