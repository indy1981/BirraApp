using AutoMapper;
using BirrasApp.Repositories.Interfaces;
using BirrasApp.Services.Interfaces;
using BirrasApp.Services.Models;
using System;
using System.Threading.Tasks;
using Repo = BirrasApp.Repositories.Models;

namespace BirrasApp.Services
{
    public class UserMeetupsService : IUserMeetupsService
    {
        private readonly IUserMeetupsRepository _userMeetupsRepository;
        private readonly IMapper _mapper;

        public UserMeetupsService(IUserMeetupsRepository userMeetupsRepository, IMapper mapper)
        {
            _userMeetupsRepository = userMeetupsRepository;
            _mapper = mapper;
        }

        public async Task<UserMeetUp> GetByUserIdAndMeetupIdAsync(int userId, int meetupId)
        {
            var userMeetupRepo = await _userMeetupsRepository.GetByUserIdAndMeetupIdAsync(userId, meetupId);
            return userMeetupRepo != null ? _mapper.Map<UserMeetUp>(userMeetupRepo) : null;
        }

        public async Task<UserMeetUp> Create(UserMeetUp userMeetUp)
        {
            var meetupRepo = _mapper.Map<Repo.UserMeetUp>(userMeetUp);
            var meetUpCreated = await _userMeetupsRepository.Create(meetupRepo);
            return meetUpCreated != null ? _mapper.Map<UserMeetUp>(meetUpCreated) : null;
        }

        public async Task<bool> UpdateCheckIn(int userId, int meetUpId, bool state)
        {
            try
            {
                var userMeetupRepo = await _userMeetupsRepository.GetByUserIdAndMeetupIdAsync(userId, meetUpId);
                userMeetupRepo.CheckedIn = state;
                _userMeetupsRepository.Update(userMeetupRepo);
                return true;
            }
            catch (Exception ex)
            {
                // log error
                return false;
            }
            
        }
    }
}
