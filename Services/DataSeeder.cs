using System.Linq;
using EduConnect.Interfaces;
using EduConnect.Models;

namespace EduConnect.Services
{
    public static class DataSeeder
    {
        public static void SeedData(IRepository<Faculty> facultyRepo, IRepository<Course> courseRepo, IRepository<Admin> adminRepo)
        {
            if (!adminRepo.GetAll().Any())
            {
                adminRepo.Add(new Admin 
                { 
                    Email = "admin@educonnect.com", 
                    Password = "password", 
                    FirstName = "System", 
                    LastName = "Admin" 
                });
            }

            if (facultyRepo.GetAll().Any()) return;

            var faculty1 = new Faculty { FirstName = "John", LastName = "Doe", Email = "faculty@educonnect.com", Password = "password", Department = "Computer Science" };
            facultyRepo.Add(faculty1);

            courseRepo.Add(new Course { Code = "CS101", Name = "Intro to Programming", Description = "Learn C# and Blazor basics.", CreditHours = 3, MaxCapacity = 2, FacultyId = faculty1.Id });
            courseRepo.Add(new Course { Code = "CS102", Name = "Data Structures", Description = "Trees, Lists, and Graphs deep dive.", CreditHours = 4, MaxCapacity = 30, FacultyId = faculty1.Id });
            courseRepo.Add(new Course { Code = "MA101", Name = "Calculus I", Description = "Limits, derivatives, and integrals.", CreditHours = 4, MaxCapacity = 40 });
        }
    }
}
