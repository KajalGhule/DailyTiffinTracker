using System.ComponentModel.DataAnnotations;

namespace TiffinTracker.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; } // "Admin", "Student", "TiffinProvider"

        public bool IsActive { get; set; } = true;

        public Student Student { get; set; } // Navigation property (optional for Student role)
    }

}
