using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.Services.Models
{
    public class MeetupWithCheckedIn : Meetup
    {
        public bool CheckedIn { get; set; }
    }
}
