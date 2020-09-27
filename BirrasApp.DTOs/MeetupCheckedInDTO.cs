using System;
using System.Collections.Generic;
using System.Text;

namespace BirrasApp.DTOs
{
    public class MeetupCheckedInDTO : MeetupDTO
    {
        public bool CheckedIn { get; set; }
    }
}
