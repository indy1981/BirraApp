using BirrasApp.Repositories.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BirrasApp.Repositories.Interfaces
{
    public interface IUserRequestToMeetUpRepository
    {
        void Update(UserRequestToMeetUp userMeetUp);
        IList<UserRequestToMeetUp> List();
        Task<UserRequestToMeetUp> Create(UserRequestToMeetUp entity);
    }
}
