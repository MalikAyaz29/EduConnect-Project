using System;
using System.Collections.Generic;
using System.Linq;
using EduConnect.Interfaces;
using EduConnect.Models;
using EduConnect.Exceptions;

namespace EduConnect.Services
{
    public class CourseService : ICourseService
    {
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Enrollment> _enrollmentRepository;
        private readonly INotificationService _notificationService;

        public event Action OnEnrollmentChanged;

        public CourseService(IRepository<Course> courseRepository, 
                             IRepository<Enrollment> enrollmentRepository,
                             INotificationService notificationService)
        {
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _notificationService = notificationService;
        }

        public IEnumerable<Course> GetAllCourses() => _courseRepository.GetAll();

        public Course GetCourseById(Guid id) => _courseRepository.GetById(id);

        public IEnumerable<Course> GetCoursesByFaculty(Guid facultyId)
        {
            return _courseRepository.Query().Where(c => c.FacultyId == facultyId);
        }

        public IEnumerable<Enrollment> GetEnrollmentsByStudent(Guid studentId)
        {
            return _enrollmentRepository.Query().Where(e => e.StudentId == studentId);
        }

        public IEnumerable<Enrollment> GetEnrollmentsByCourse(Guid courseId)
        {
            return _enrollmentRepository.Query().Where(e => e.CourseId == courseId);
        }

        public void EnrollStudent(Guid studentId, Guid courseId)
        {
            var course = _courseRepository.GetById(courseId);
            if (course == null) return;

            // Business Rule: Check capacity
            if (course.Status == Enums.CourseStatus.Full)
            {
                throw new CourseFullException();
            }

            // Check if already enrolled
            var existing = _enrollmentRepository.Query()
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId && e.Status == Enums.EnrollmentStatus.Active);
            
            if (existing != null) return;

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId
            };

            _enrollmentRepository.Add(enrollment);
            
            // Update course enrollment count
            course.CurrentEnrollmentCount++;
            _courseRepository.Update(course);

            // Trigger Event
            OnEnrollmentChanged?.Invoke();

            // Trigger Notification
            _notificationService.SendNotification(studentId, "Course Enrollment", $"You have successfully enrolled in {course.Name}.", Enums.NotificationType.Enrollment);
        }

        public void DropCourse(Guid studentId, Guid courseId)
        {
            var enrollment = _enrollmentRepository.Query()
                .FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId && e.Status == Enums.EnrollmentStatus.Active);

            if (enrollment != null)
            {
                enrollment.Status = Enums.EnrollmentStatus.Dropped;
                _enrollmentRepository.Update(enrollment);

                var course = _courseRepository.GetById(courseId);
                if (course != null)
                {
                    course.CurrentEnrollmentCount--;
                    _courseRepository.Update(course);
                }

                OnEnrollmentChanged?.Invoke();
            }
        }
    }
}
