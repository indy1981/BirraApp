using BirrasApp.Repositories.Interfaces;
using BirrasApp.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BirrasApp.Repositories
{
    public class UserRequestToMeetUpRepository : EFBaseRepository<UserRequestToMeetUp, int>, IUserRequestToMeetUpRepository
    {
        public UserRequestToMeetUpRepository(BirrasAppContext context) : base(context) { }

        public override IList<UserRequestToMeetUp> List()
        {
            return _dbContext.Set<UserRequestToMeetUp>().Include(request => request.Meetup).Include(request => request.User).ToList();
        }
    }
}
