using BirrasApp.Services.Models.Enums;
using System;

namespace BirrasApp.Services.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UsersRoles UserRol { get; set; }
    }
}
