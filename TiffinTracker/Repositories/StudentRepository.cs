//using TiffinTracker.Data;
using TiffinTracker.Db;
using TiffinTracker.Models;
using TiffinTracker.Repositories.Interfaces;

namespace TiffinTracker.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Student> GetAll() => _context.Student.ToList();

        public Student GetById(int id) => _context.Student.Find(id);

        public void Add(Student student)
        {
            _context.Student.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Student.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = _context.Student.Find(id);
            if (student != null)
            {
                _context.Student.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
