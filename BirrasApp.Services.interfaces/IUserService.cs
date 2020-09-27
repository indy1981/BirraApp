using BirrasApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BirrasApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Login(User user);
        Task<User> Register(User user);
        Task<User> GetByUsername(string username);
        IList<User> GetAllNonAdminUsers();
    }
}
