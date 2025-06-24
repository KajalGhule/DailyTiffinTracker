using System;

namespace TiffinTracker.Models
{
    public class MealAuditLog
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Action { get; set; }

        public int? PerformedBy { get; set; }

        public DateTime ActionTime { get; set; } = DateTime.Now;

        public Student Student { get; set; }

        public User PerformedByUser { get; set; }
    }

}
