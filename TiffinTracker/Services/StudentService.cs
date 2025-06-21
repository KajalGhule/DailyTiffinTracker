using TiffinTracker.Models;
using TiffinTracker.Repositories.Interfaces;
using TiffinTracker.Services.Interfaces;

namespace TiffinTracker.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public List<Student> GetAllStudents() => _repository.GetAll();

        public Student GetStudentById(int id) => _repository.GetById(id);

        public void CreateStudent(Student student) => _repository.Add(student);

        public void UpdateStudent(Student student) => _repository.Update(student);

        public void DeleteStudent(int id) => _repository.Delete(id);
    }
}
