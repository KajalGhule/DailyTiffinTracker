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
        // POST: /Student/AddTodayMeal
        [HttpPost]
        public IActionResult AddTodayMeal(string mealType, string remarks)
        {
            var userIdClaim = User.FindFirst("UserId"); // "UserId" must be a claim in your JWT
            if (userIdClaim == null)
                return Unauthorized();

            int userId = int.Parse(userIdClaim.Value);

            var student = _context.Student.FirstOrDefault(s => s.UserId == userId); // Replace with actual UserId

            if (student == null) return NotFound();

            if (!Enum.TryParse(mealType, out MealType parsedMealType))
                return BadRequest("Invalid meal type.");

            var meal = new MealDistribution
            {
                StudentId = student.Id,
                MealType = parsedMealType,
                DistributionDate = DateTime.Today,
                Received = true,
                Remarks = remarks,
                CreatedBy = student.UserId
            };

            _context.MealDistribution.Add(meal);
            _context.SaveChanges();

            return RedirectToAction("History");
        }
    }


}
