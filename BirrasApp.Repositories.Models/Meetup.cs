using System;
using System.Collections.Generic;

namespace BirrasApp.Repositories.Models
{
    public class Meetup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public DateTimeOffset MeetupDate { get; set; }

        public float Temperature { get; set; }

        public virtual ICollection<UserMeetUp> UserMeetUps { get; set; }

        public virtual ICollection<UserRequestToMeetUp> Requests { get; set; }
    }
}
