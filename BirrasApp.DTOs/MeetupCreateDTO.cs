using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BirrasApp.DTOs
{
    public class MeetupCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public string Subject { get; set; }

        [Required]
        public DateTimeOffset MeetupDate { get; set; }

        public IList<int> InviteesIds { get; set; }
    }
}