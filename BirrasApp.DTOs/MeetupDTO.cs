using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BirrasApp.DTOs
{
    public class MeetupDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Subject { get; set; }

        [Required]
        public DateTimeOffset MeetupDate { get; set; }

        [Required]
        public float Temperature { get; set; }
    }
}
