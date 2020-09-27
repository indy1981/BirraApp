using BirrasApp.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BirrasApp.Services.Interfaces
{
    public interface IUserRequestToMeetUpService
    {
        bool Update(UserRequestToMeetUp requestToMeetUp);

        IList<UserRequestToMeetUp> List();

        Task<UserRequestToMeetUp> Create(UserRequestToMeetUp userRequestToMeetUp);
    }
}
