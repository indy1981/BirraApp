using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using BirrasApp.Repositories.Interfaces;
using Repo = BirrasApp.Repositories.Models;
using Logic = BirrasApp.Services.Models;
using BirrasApp.Services.Interfaces;
using System.Collections.Generic;
using BirrasApp.Services.Tools;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BirrasApp.Services
{
    public class MeetupsService : IMeetupsService
    {
        private readonly IMeetupsRepository _meetupsRepository;
        private readonly IMapper _mapper;

        public MeetupsService(IMeetupsRepository meetupsRepository, IMapper mapper)
        {
            _meetupsRepository = meetupsRepository;
            _mapper = mapper;
        }

        public Logic.Meetup GetAsync(Expression<Func<Logic.Meetup, bool>> predicate)
        {
            var meetupRepoQueryble = _meetupsRepository.List();
            var meetUpLogicQueryble = _mapper.Map<IQueryable<Logic.Meetup>>(meetupRepoQueryble);
            return meetUpLogicQueryble.FirstOrDefault(predicate);
        }

        public async Task<Logic.Meetup> Create(Logic.Meetup meetup)
        {            
            var meetupRepo = _mapper.Map<Repo.Meetup>(meetup);
            try
            {
                var meetupCreated = await _meetupsRepository.Create(meetupRepo);
                if (meetupCreated == null)
                    return null;

                var meetupResponse = _mapper.Map<Logic.Meetup>(meetupCreated);
                meetupResponse.BirrasPack = BirrasLogic.GetTotalPacksByTemperatureAndPeople(meetupResponse.Temperature, meetup.InviteesIds.Count);
                return meetupResponse;
            }
            catch(Exception ex)
            {
                //logguar el error aqui
                return null;
            }            
        }

        public IList<Logic.Meetup> GetAllMeetupsUserWasNotInvited(int userId)
        {
            var userMeetupsUserWasNotInvited = _meetupsRepository.QueryableWithUserMeetups(x => !x.UserMeetUps.Any( userMeetup => userMeetup.UserId == userId));
            if (userMeetupsUserWasNotInvited == null)
            {
                return null;
            }
            return _mapper.Map<IList<Logic.Meetup>>(userMeetupsUserWasNotInvited.ToList());
        }

        public IList<Logic.Meetup> List()
        {
            var meetupRepoQueryble = _meetupsRepository.List();
            var meetupsLogic = _mapper.Map<IList<Logic.Meetup>>(meetupRepoQueryble);
            foreach (var meetup in meetupsLogic)
            {
                meetup.BirrasPack = BirrasLogic.GetTotalPacksByTemperatureAndPeople(meetup.Temperature, meetup.InviteesIds.Count);
            }

            return meetupsLogic;
        }


        // borrar ?
        public IList<Logic.Meetup> ListRequestsByUserId(int userId)
        {
            var meetupQuerybleRepo = _meetupsRepository.QueryableWithUserMeetups( m => m.Requests.Any(r => r.UserId == userId));
            if (meetupQuerybleRepo == null)
            {
                return null;
            }
            return _mapper.Map<IList<Logic.Meetup>>(meetupQuerybleRepo);
        }

        // borrar ?
        public IList<Logic.MeetupWithCheckedIn> ListInvitesByUserId(int userId)
        {
            var meetupsWithCheckedinList = _meetupsRepository.QueryableWithUserMeetups(
                    m => m.UserMeetUps.Any(u => u.UserId == userId && u.MeetupId == m.Id))
                .Select(x => new Logic.MeetupWithCheckedIn()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        MeetupDate = x.MeetupDate,
                        Temperature = x.Temperature,
                        CheckedIn = x.UserMeetUps.FirstOrDefault(user => user.UserId == userId).CheckedIn
                    }).ToList();

            return meetupsWithCheckedinList;
        }

        public async Task<bool> CheckAsistanceForMeetup(int userId, int meetupId)
        {
            try
            {
                var meetup = await _meetupsRepository.GetByIdAsync(meetupId);
                if (meetup == null)
                    return false;

                var userMeetup = meetup.UserMeetUps.FirstOrDefault(um => um.UserId == userId);

                if (userMeetup == null)
                    return false;

                userMeetup.CheckedIn = true;

                _meetupsRepository.Update(meetup);
                return true;
            }
            catch (Exception ex)
            {
                // loggear Exception
                return false;
            }
        }
    }
}
