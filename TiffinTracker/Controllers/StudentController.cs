using Microsoft.AspNetCore.Mvc;
using TiffinTracker.Models;
//using TiffinTracker.Services.Interfaces;
using TiffinTracker.Services.Interfaces;

namespace TiffinTracker.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        public IActionResult GetAllStudents()
        {
            var students = _service.GetAllStudents();
            return View(students);
        }
    }
}
