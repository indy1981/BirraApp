using BirrasApp.Repositories.Interfaces;
using BirrasApp.Repositories.Models;
using System.Threading.Tasks;

namespace BirrasApp.Repositories
{
    public class UserMeetupsRepository : EFBaseRepository<UserMeetUp, int>, IUserMeetupsRepository
    {
        public UserMeetupsRepository(BirrasAppContext context) : base(context) { }

        public async Task<UserMeetUp> GetByUserIdAndMeetupIdAsync(int userId, int meetupId)
        {
            return await base.GetAsync(x => x.UserId == userId && x.MeetupId == meetupId);
        }
    }
}
