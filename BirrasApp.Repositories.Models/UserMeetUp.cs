using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.Repositories.Models
{
    public class UserMeetUp
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int MeetupId { get; set; }

        public Meetup Meetup { get; set; }

        public bool CheckedIn { get; set; }
    }
}
