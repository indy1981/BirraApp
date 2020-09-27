using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BirrasApp.DTOs
{
    public class MeetupAdminDTO : MeetupDTO
    {
        public IList<int> InviteesIds { get; set; }

        public int BirrasPack { get; set; }
    }
}
