using System;
using System.Collections.Generic;
using System.Linq;
using EduConnect.Interfaces;
using EduConnect.Models;

namespace EduConnect.Services
{
    public class GradeService : IGradeService
    {
        private readonly IRepository<GradeRecord> _gradeRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly INotificationService _notificationService;
        private readonly IRepository<Student> _studentRepository;

        public event Action OnGradesSubmitted;

        public GradeService(IRepository<GradeRecord> gradeRepository, 
                            IRepository<Course> courseRepository,
                            INotificationService notificationService,
                            IRepository<Student> studentRepository)
        {
            _gradeRepository = gradeRepository;
            _courseRepository = courseRepository;
            _notificationService = notificationService;
            _studentRepository = studentRepository;
        }

        public IEnumerable<GradeRecord> GetGradesByStudent(Guid studentId)
        {
            return _gradeRepository.Query().Where(g => g.StudentId == studentId);
        }

        public IEnumerable<GradeRecord> GetGradesByCourse(Guid courseId)
        {
            return _gradeRepository.Query().Where(g => g.CourseId == courseId);
        }

        public void SubmitGrade(Guid studentId, Guid courseId, double marks)
        {
            var existingGrade = _gradeRepository.Query()
                .FirstOrDefault(g => g.StudentId == studentId && g.CourseId == courseId);

            if (existingGrade != null)
            {
                existingGrade.Marks = marks;
                _gradeRepository.Update(existingGrade);
            }
            else
            {
                // Note: In a robust setup, you'd find the EnrollmentId first
                var gradeRecord = new GradeRecord
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    Marks = marks
                };
                _gradeRepository.Add(gradeRecord);
            }

            OnGradesSubmitted?.Invoke();

            // Notify student
            var course = _courseRepository.GetById(courseId);
            string courseName = course != null ? course.Name : "a course";
            _notificationService.SendNotification(studentId, "Grade Posted", $"Your grade for {courseName} has been posted.", Enums.NotificationType.Grade);
        }

        public double CalculateCGPA(Guid studentId)
        {
            var grades = GetGradesByStudent(studentId).ToList();
            if (!grades.Any())
            {
                var student = _studentRepository.GetById(studentId);
                return student != null ? student.CGPA : 0.0;
            }

            double totalPoints = 0;
            int totalCredits = 0;

            foreach (var grade in grades)
            {
                var course = _courseRepository.GetById(grade.CourseId);
                if (course != null)
                {
                    totalPoints += grade.GradePoints * course.CreditHours;
                    totalCredits += course.CreditHours;
                }
            }

            return totalCredits == 0 ? 0 : totalPoints / totalCredits;
        }
    }
}
