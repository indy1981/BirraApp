using BirrasApp.DTOs.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace BirrasApp.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MaxLength(30)]
        [MinLength(8)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
