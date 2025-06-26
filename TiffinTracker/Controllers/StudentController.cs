using Microsoft.AspNetCore.Mvc;
using TiffinTracker.Db;
using TiffinTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TiffinTracker.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Welcome()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: /Student/History
        [Authorize(Roles = "Student")]
        public IActionResult History()
        {
            var userIdClaim = User.FindFirst("UserId"); // "UserId" must be a claim in your JWT
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var student = _context.Student
                .Include(s => s.MealDistributions)
                .FirstOrDefault(s => s.UserId == userId);

            if (student == null)
                return NotFound();

            return View(student);
        }
        
        public IActionResult AddTodayMeal()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult AddTodayMeal(string mealType, string? remarks, bool received)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var student = _context.Student.FirstOrDefault(s => s.UserId == userId);
            if (student == null) return NotFound("Student not found.");

            if (!Enum.TryParse(mealType, true, out MealType parsedMealType))
                return BadRequest("Invalid meal type.");

            // Only allow tiffin before 12 PM
            if (parsedMealType == MealType.AfternoonTiffin && DateTime.Now.TimeOfDay > new TimeSpan(14, 0, 0))
                return BadRequest("Tiffin can only be requested before 12:00 PM.");

            if (parsedMealType == MealType.EveningTiffin && DateTime.Now.TimeOfDay > new TimeSpan(19, 0, 0))
                return BadRequest("Tiffin can only be requested before 7:00 PM.");

            // Prevent duplicate entry
            bool alreadyExists = _context.MealDistribution.Any(m =>
                m.StudentId == student.Id &&
                m.MealType == parsedMealType &&
                m.DistributionDate == DateTime.Today);

            if (alreadyExists)
                return BadRequest("You have already requested this meal today.");

            // ❗ Remarks are only allowed if received == true
            if (!received && !string.IsNullOrWhiteSpace(remarks))
                return BadRequest("Remarks are allowed only if the meal is marked as received.");

            var meal = new MealDistribution
            {
                StudentId = student.Id,
                MealType = parsedMealType,
                DistributionDate = DateTime.Today,
                Received = received,
                Remarks = received ? remarks?.Trim() : null,
                CreatedBy = student.UserId
            };

            Console.WriteLine("Received value: " + meal.Received);
            _context.MealDistribution.Add(meal);
            _context.SaveChanges();

            //return Ok("Tiffin request submitted.");
            return RedirectToAction("Dashboard");
            
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public IActionResult UpdateMealStatus(int mealId, bool received, string? remarks)
        {
            var meal = _context.MealDistribution.FirstOrDefault(m => m.Id == mealId);
            if (meal == null)
                return NotFound("Meal not found.");

            // Ensure the logged-in user owns this meal
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var student = _context.Student.FirstOrDefault(s => s.UserId == userId);
            if (student == null || meal.StudentId != student.Id)
                return Unauthorized();

            // Remarks allowed only if received = true
            if (!received && !string.IsNullOrWhiteSpace(remarks))
                return BadRequest("Remarks are only allowed when meal is received.");

            meal.Received = received;
            meal.Remarks = received ? remarks?.Trim() : null;

            _context.SaveChanges();

            return RedirectToAction("History");
            
        }

    }

}
