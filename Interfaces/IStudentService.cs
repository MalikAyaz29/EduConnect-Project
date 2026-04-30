using System;
using System.Collections.Generic;
using EduConnect.Models;

namespace EduConnect.Interfaces
{
    // SOLID Principle: Interface Segregation Principle (ISP)
    // Specific interface for student-related operations instead of a giant generic service.
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        IEnumerable<Student> SearchStudents(string searchTerm);
        Student GetStudentById(Guid id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(Guid id);
        event Action OnStudentUpdated;
    }
}
