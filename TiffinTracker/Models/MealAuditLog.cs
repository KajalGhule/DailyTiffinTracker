using System;

namespace TiffinTracker.Models
{
    public class MealAuditLog
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Action { get; set; }

        public DateTime ActionTime { get; set; } = DateTime.Now;

        public Student Student { get; set; }
    }
}
