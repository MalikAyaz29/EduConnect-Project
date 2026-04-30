using System;
using System.Collections.Generic;
using EduConnect.Models;

namespace EduConnect.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(Guid id);
        IEnumerable<Course> GetCoursesByFaculty(Guid facultyId);
        
        IEnumerable<Enrollment> GetEnrollmentsByStudent(Guid studentId);
        IEnumerable<Enrollment> GetEnrollmentsByCourse(Guid courseId);
        
        void EnrollStudent(Guid studentId, Guid courseId);
        void DropCourse(Guid studentId, Guid courseId);

        event Action OnEnrollmentChanged;
    }
}
