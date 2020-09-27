using AutoMapper;
using BirrasApp.Repositories.Interfaces;
using BirrasApp.Services.Interfaces;
using BirrasApp.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repo = BirrasApp.Repositories.Models;

namespace BirrasApp.Services
{
    public class UserRequestToMeetUpService : IUserRequestToMeetUpService
    {
        private readonly IUserRequestToMeetUpRepository _userRequestToMeetUpRepository;
        private readonly IMapper _mapper;

        public UserRequestToMeetUpService(IUserRequestToMeetUpRepository userRequestToMeetUpRepository, IMapper mapper)
        {
            _userRequestToMeetUpRepository = userRequestToMeetUpRepository;
            _mapper = mapper;
        }

        public IList<UserRequestToMeetUp> List()
        {
            var userRequestsListRepo = _userRequestToMeetUpRepository.List();
            if (userRequestsListRepo == null)
                return null;

            return _mapper.Map<IList<UserRequestToMeetUp>>(userRequestsListRepo);
        }

        public async Task<UserRequestToMeetUp> Create(UserRequestToMeetUp userRequestToMeetUp)
        {
            try
            {
                var userMeetupRepo = _mapper.Map<Repo.UserRequestToMeetUp>(userRequestToMeetUp);
                var createdUserMeetup = await _userRequestToMeetUpRepository.Create(userMeetupRepo);
                if (createdUserMeetup == null)
                    return null;

                return _mapper.Map<UserRequestToMeetUp>(createdUserMeetup);
            }
            catch (Exception)
            {
                // log error aqui
                return null;
            }
        }

        public bool Update(UserRequestToMeetUp userRequestToMeetUp)
        {
            try
            {
                var userMeetupRepo = _mapper.Map<Repo.UserRequestToMeetUp>(userRequestToMeetUp);
                _userRequestToMeetUpRepository.Update(userMeetupRepo);
                return true;
            }
            catch (Exception)
            {
                // log error aqui
                return false;
            }            
        }
    }
}
