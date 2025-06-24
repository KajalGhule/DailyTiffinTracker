using Microsoft.AspNetCore.Mvc;
using TiffinTracker.Db;
using TiffinTracker.Models;
using TiffinTracker.Services;

namespace TiffinTracker.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = _context.User.FirstOrDefault(u =>
                u.Username == model.Username && u.PasswordHash == model.Password); // You’ll hash this later

            if (user == null)
            {
                ViewBag.Error = "Invalid credentials";
                return View(model);
            }

            var token = _jwtService.GenerateToken(user);
            ViewBag.Token = token;
            ViewBag.Role = user.Role;

            return View("LoginSuccess");
        }
    }

}
