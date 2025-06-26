using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiffinTracker.Models
{
    public enum MealType
    {
        Breakfast,
        Tiffin
    }

    public class MealDistribution
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        public DateTime DistributionDate { get; set; }

        [Required]
        public MealType MealType { get; set; } // "Breakfast", "Tiffin"

        public bool Received { get; set; }

        public string? Remarks { get; set; }

        
        public int? CreatedBy { get; set; }

        public Student Student { get; set; }
        [ForeignKey("CreatedBy")]
        public User Creator { get; set; }
    }

}
