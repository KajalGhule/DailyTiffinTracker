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

        [Required]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Required]
        public DateTime DistributionDate { get; set; }

        [Required]
        public MealType MealType { get; set; }

        public bool Received { get; set; }

        public string Remarks { get; set; }
    }
}
