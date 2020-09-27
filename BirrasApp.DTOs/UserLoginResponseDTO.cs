using BirrasApp.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.DTOs
{
    public class UserLoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public UsersRoles UserRol { get; set; }

        public string AccessToken { get; set; }
    }
}
