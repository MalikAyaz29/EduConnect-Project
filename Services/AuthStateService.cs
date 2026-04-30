using System;
using System.Linq;
using EduConnect.Models;
using EduConnect.Interfaces;

namespace EduConnect.Services
{
    public class AuthStateService : IAuthService
    {
        private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<Faculty> _facultyRepo;
        private readonly IRepository<Admin> _adminRepo;

        public Person CurrentUser { get; private set; }

        public event Action OnLogin;
        public event Action OnLogout;

        public AuthStateService(IRepository<Student> studentRepo, 
                                IRepository<Faculty> facultyRepo, 
                                IRepository<Admin> adminRepo)
        {
            _studentRepo = studentRepo;
            _facultyRepo = facultyRepo;
            _adminRepo = adminRepo;
        }

        public bool Login(string email, string password)
        {
            var admin = _adminRepo.Query().FirstOrDefault(u => u.Email == email && u.Password == password);
            if (admin != null) { SetUser(admin); return true; }

            var faculty = _facultyRepo.Query().FirstOrDefault(u => u.Email == email && u.Password == password);
            if (faculty != null) { SetUser(faculty); return true; }

            var student = _studentRepo.Query().FirstOrDefault(u => u.Email == email && u.Password == password);
            if (student != null) { SetUser(student); return true; }

            return false;
        }

        public void Logout()
        {
            CurrentUser = null;
            OnLogout?.Invoke();
        }

        private void SetUser(Person user)
        {
            CurrentUser = user;
            OnLogin?.Invoke();
        }

        public bool IsLoggedIn => CurrentUser != null;
    }
}
