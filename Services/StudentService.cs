using System;
using System.Collections.Generic;
using System.Linq;
using EduConnect.Interfaces;
using EduConnect.Models;
using EduConnect.Exceptions;

namespace EduConnect.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;

        // SOLID Principle: Dependency Inversion Principle (DIP)
        public StudentService(IRepository<Student> studentRepository, IRepository<Enrollment> enrollmentRepository)
        {
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        public IEnumerable<Student> GetAllStudents() => _studentRepository.GetAll();

        public IEnumerable<Student> SearchStudents(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return GetAllStudents();
            return _studentRepository.Query()
                .Where(s => s.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                            s.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }

        public Student GetStudentById(Guid id) => _studentRepository.GetById(id);

        public void AddStudent(Student student)
        {
            _studentRepository.Add(student);
        }

        public event Action OnStudentUpdated;

        public void UpdateStudent(Student student)
        {
            _studentRepository.Update(student);
            OnStudentUpdated?.Invoke();
        }

        public void DeleteStudent(Guid id)
        {
            // Business Rule: Check for active enrollments before deleting a student
            var activeEnrollments = _enrollmentRepository.Query()
                .Any(e => e.StudentId == id && e.Status == Enums.EnrollmentStatus.Active);
                
            if (activeEnrollments)
            {
                throw new StudentHasActiveEnrollmentsException();
            }

            _studentRepository.Delete(id);
        }
    }
}
