using System;
using System.Collections.Generic;
using EduConnect.Enums;
using EduConnect.Interfaces;

namespace EduConnect.Models
{
    // SOLID Principle: Liskov Substitution Principle (LSP)
    // Student can replace Person without altering the correctness of the program.
    public class Student : Person, IValidatable
    {
        public override Role Role => Role.Student;

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        public string Major { get; set; } = string.Empty;
        public int Semester { get; set; } = 1;
        public double CGPA { get; set; } = 0.0;

        public bool IsValid()
        {
            var errors = GetValidationErrors() as List<string>;
            return errors.Count == 0;
        }

        public IEnumerable<string> GetValidationErrors()
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(FirstName)) errors.Add("First Name is required.");
            if (string.IsNullOrWhiteSpace(LastName)) errors.Add("Last Name is required.");
            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@")) errors.Add("Valid Email is required.");
            if (string.IsNullOrWhiteSpace(Password)) errors.Add("Password is required.");
            return errors;
        }
    }
}
