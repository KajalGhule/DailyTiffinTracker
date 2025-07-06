using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiffinTracker.Db;
using TiffinTracker.Models;


namespace TiffinTracker.Controllers
{
    [Route("provider")]
    public class TiffinProviderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiffinProviderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet("today-tiffin")]
        //[HttpGet]
        public IActionResult GetTodayTiffin()
        {
            int providerId = Convert.ToInt32(User.FindFirst("UserId")?.Value); // from session/JWT

            var today = DateTime.Today;

            var todayMeals = _context.MealDistribution
                .Where(md => md.DistributionDate == today)
                .Join(_context.ProviderStudent,
                      md => md.StudentId,
                      ps => ps.StudentId,
                      (md, ps) => new { md, ps })
                .Where(x => x.ps.ProviderId == providerId)
                .Join(_context.Student,
                      x => x.md.StudentId,
                      s => s.Id,
                      (x, s) => new TodayMealDto
                      {
                          StudentName = s.Name,
                          MealType = x.md.MealType.ToString(),
                          Received = x.md.Received,
                          Remarks = x.md.Remarks
                      })
                .ToList();

            return View(todayMeals);
        }

        [HttpGet("tiffin-count")]
        public IActionResult GetTiffinCountForToday()
        {
            int providerId = Convert.ToInt32(User.FindFirst("UserId")?.Value);

            var today = DateTime.Today;

            // Get count of meals to be prepared for today by this provider
            //var count = _context.MealDistribution
            //    .Where(md => md.DistributionDate == today)
            //    .Join(_context.ProviderStudent,
            //          md => md.StudentId,
            //          ps => ps.StudentId,
            //          (md, ps) => new { md, ps })
            //    .Count(x => x.ps.ProviderId == providerId);

            //ViewBag.TiffinCount = count;

            var groupedCounts = _context.MealDistribution
                        .Where(md => md.DistributionDate == today)
                        .Join(_context.ProviderStudent,
                                md => md.StudentId,
                                ps => ps.StudentId,
                               (md, ps) => new { md, ps })
                        .Where(x => x.ps.ProviderId == providerId)
                        .GroupBy(x => x.md.MealType)
                        .Select(g => new
                                {
                                    MealType = g.Key,
                                    Count = g.Count()
                                })
                        .ToList();

               ViewBag.GroupedTiffinCounts = groupedCounts;


            return View(); 
        }
    }
}
