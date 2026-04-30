using System;
using EduConnect.Models;

namespace EduConnect.Interfaces
{
    public interface IAuthService
    {
        Person CurrentUser { get; }
        bool IsLoggedIn { get; }
        
        bool Login(string email, string password);
        void Logout();

        event Action OnLogin;
        event Action OnLogout;
    }
}
