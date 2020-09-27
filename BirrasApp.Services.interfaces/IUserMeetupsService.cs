using BirrasApp.Services.Models;
using System.Threading.Tasks;

namespace BirrasApp.Services.Interfaces
{
    public interface IUserMeetupsService
    {
        Task<bool> UpdateCheckIn(int userId, int meetUpId, bool state);

        Task<UserMeetUp> GetByUserIdAndMeetupIdAsync(int userId, int meetupId);

        Task<UserMeetUp> Create(UserMeetUp userMeetUp);
    }
}
