using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirrasApp.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
