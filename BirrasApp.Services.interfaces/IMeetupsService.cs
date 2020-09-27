using BirrasApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BirrasApp.Services.Interfaces
{
    public interface IMeetupsService
    {
        IList<Meetup> List();
        IList<Meetup> GetAllMeetupsUserWasNotInvited(int userId);
        IList<MeetupWithCheckedIn> ListInvitesByUserId(int userId);
        IList<Meetup> ListRequestsByUserId(int userId);
        Task<Meetup> Create(Meetup meetup);
        Task<bool> CheckAsistanceForMeetup(int userId, int meetupId);
    }
}