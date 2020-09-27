using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.DTOs
{
    public class UserRequestToMeetUpDTO
    {
        public int UserId { get; set; }

        public UserDTO User { get; set; }

        public int MeetupId { get; set; }

        public MeetupDTO Meetup { get; set; }
    }
}
