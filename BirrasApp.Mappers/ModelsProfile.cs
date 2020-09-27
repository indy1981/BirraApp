using AutoMapper;
using Logic = BirrasApp.Services.Models;
using Persistance = BirrasApp.Repositories.Models;
using DTOs = BirrasApp.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace BirrasApp.Mappers
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<Logic.User, Persistance.User>()
                .ForMember(d => d.PasswordHash, o=> o.Ignore())
                .ForMember(d => d.PasswordSalt, o => o.Ignore())
                .ReverseMap()
                .ForMember(d => d.Password, o => o.Ignore());

            CreateMap<Logic.UserRequestToMeetUp, Persistance.UserRequestToMeetUp>()
                .ReverseMap();

            CreateMap<DTOs.UserRequestToMeetUpDTO, Logic.UserRequestToMeetUp>()
                .ReverseMap();

            CreateMap<DTOs.UserMeetupDTO, Logic.UserMeetUp>()
                .ReverseMap();

            

            CreateMap<Logic.UserMeetUp, Persistance.UserMeetUp>()
                .ReverseMap();

            CreateMap<DTOs.UserDTO, Logic.User>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ReverseMap();

            CreateMap<DTOs.MeetupAdminDTO, Logic.Meetup>()                
                .ReverseMap();

            CreateMap<DTOs.MeetupDTO, Logic.Meetup>()
                .ReverseMap();

            CreateMap<Logic.MeetupWithCheckedIn, DTOs.MeetupCheckedInDTO>();

            CreateMap<DTOs.MeetupCreateDTO, Logic.Meetup>()
                    .ForMember(d => d.BirrasPack, o => o.Ignore())
                    .ForMember(d => d.Id, o => o.Ignore())
                .ReverseMap();

            CreateMap<Logic.Meetup, Persistance.Meetup>()
                    .ForMember(d => d.UserMeetUps, o => o.MapFrom(list => CreateMeetupsListFromIds(list.InviteesIds)))
                .ReverseMap()
                    .ForMember( d => d.InviteesIds, o => o.MapFrom( list => CreateListOfIdsFromInvitees(list.UserMeetUps)));


            CreateMap<DTOs.UserLoginDTO, Logic.User>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.UserRol, o => o.Ignore())
                .ReverseMap();

            CreateMap<Logic.User, DTOs.UserLoginResponse>()
                .ForMember(d => d.AccessToken, o => o.Ignore());            
        }

        // mejorarrrrrrrrrrrrr

        private IList<Persistance.UserMeetUp> CreateMeetupsListFromIds(IList<int> userIds)
        {
            IList<Persistance.UserMeetUp> userMeetupsList = new List<Persistance.UserMeetUp>();

            foreach (int id in userIds)
            {
                userMeetupsList.Add(new Persistance.UserMeetUp
                {
                    UserId = id
                });
            }

            return userMeetupsList;
        }

        private IList<int> CreateListOfIdsFromInvitees(ICollection<Persistance.UserMeetUp> userMeetUps)
        {
            return userMeetUps.Select(um => um.UserId).ToList();
        }
    }
}
