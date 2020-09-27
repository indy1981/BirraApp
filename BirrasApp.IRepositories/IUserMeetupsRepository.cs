using BirrasApp.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirrasApp.Repositories.Interfaces
{
    public interface IUserMeetupsRepository
    {
        void Update(UserMeetUp userMeetUp);

        Task<UserMeetUp> GetByUserIdAndMeetupIdAsync(int userId, int meetupId);

        Task<UserMeetUp> Create(UserMeetUp entity);
    }
}
