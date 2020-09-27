using System.ComponentModel.DataAnnotations;

namespace BirrasApp.DTOs
{
    public class UserMeetupDTO
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int MeetupId { get; set; }
    }
}
