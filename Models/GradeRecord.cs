using System;

namespace EduConnect.Models
{
    // SOLID Principle: Single Responsibility Principle (SRP)
    // Responsible only for tracking the grade of a student in a specific course.
    public class GradeRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EnrollmentId { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public double Marks { get; set; } // 0 - 100

        // Business Rule: Compute Letter Grade
        public string LetterGrade
        {
            get
            {
                if (Marks >= 85) return "A";
                if (Marks >= 70) return "B";
                if (Marks >= 55) return "C";
                if (Marks >= 45) return "D";
                return "F";
            }
        }

        // Business Rule: Compute Grade Points
        public double GradePoints
        {
            get
            {
                if (Marks >= 85) return 4.0;
                if (Marks >= 70) return 3.0;
                if (Marks >= 55) return 2.0;
                if (Marks >= 45) return 1.0;
                return 0.0;
            }
        }
    }
}
