using System;
using EduConnect.Enums;

namespace EduConnect.Models
{
    // SOLID Principle: Single Responsibility Principle (SRP)
    // This abstract class is only responsible for defining the common attributes of any person in the system.
    public abstract class Person
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // In a real app, this should be hashed
        
        public abstract Role Role { get; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
