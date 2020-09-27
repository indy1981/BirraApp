using BirrasApp.Repositories.Models.Enums;
using System.Collections.Generic;

namespace BirrasApp.Repositories.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public UsersRoles UserRol { get; set; }

        public string Email { get; set; }
    }
}