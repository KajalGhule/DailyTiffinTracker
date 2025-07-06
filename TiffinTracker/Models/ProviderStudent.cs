namespace TiffinTracker.Models
{
    public class ProviderStudent
    {
        public int Id { get; set; }

        // Foreign Key to User (Provider)
        public int ProviderId { get; set; }

        // Foreign Key to Student
        public int StudentId { get; set; }

        // Navigation Properties
        public User Provider { get; set; }
        public Student Student { get; set; }
    }

}
