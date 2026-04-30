using System;
using EduConnect.Enums;

namespace EduConnect.Models
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreditHours { get; set; }
        public int MaxCapacity { get; set; }
        public int CurrentEnrollmentCount { get; set; }
        public Guid? FacultyId { get; set; }

        // Business Rule: Computed property to determine if the course is full
        public CourseStatus Status => CurrentEnrollmentCount >= MaxCapacity ? CourseStatus.Full : CourseStatus.Open;
    }
}
