using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.Services.Models
{
    public class Meetup
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Subject { get; set; }

        public DateTimeOffset MeetupDate { get; set; }

        public float Temperature { get; set; }

        public int BirrasPack { get; set; }

        public IList<int> InviteesIds { get; set; }
    }
}
