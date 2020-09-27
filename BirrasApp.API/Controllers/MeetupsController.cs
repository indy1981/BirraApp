using AutoMapper;
using BirrasApp.DTOs;
using BirrasApp.External.Services.Interfaces;
using BirrasApp.Services.Interfaces;
using BirrasApp.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BirrasApp.API.Controllers
{
    [Route("api/meetups")]    
    [ApiController]
    [Authorize]
    public class MeetupsController : ControllerBase
    {
        private readonly IMeetupsService _meetupsService;
        private readonly IUserMeetupsService _userMeetupsService;
        private readonly IUserRequestToMeetUpService _requestToMeetUpService;        
        private readonly IWeatherService _openWeatherService;        
        private readonly IMapper _mapper;
        
        public MeetupsController(IMeetupsService meetupsService, 
            IWeatherService openWeatherService, 
            IMapper mapper, IUserMeetupsService 
            userMeetupsService, 
            IUserRequestToMeetUpService requestToMeetUpService)
        {
            _meetupsService = meetupsService;
            _openWeatherService = openWeatherService;
            _userMeetupsService = userMeetupsService;
            _requestToMeetUpService = requestToMeetUpService;
            _mapper = mapper;
        }

        [HttpGet("user/{userId}/me/invites")]
        [ProducesResponseType(typeof(IList<MeetupAdminDTO>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public ActionResult GetAllEventsInvitesByUser([FromRoute] int userId)  // ver si es admin solo o no
        {
            if (!CheckUserIdFromClaims(userId))
            {
                return Unauthorized();
            }
            var meetupsFromRepo = _meetupsService.ListInvitesByUserId(userId);
            if (meetupsFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<IList<MeetupCheckedInDTO>>(meetupsFromRepo));
        }

        [HttpGet("user/{userId}/me/not/invites")]
        [ProducesResponseType(typeof(IList<MeetupAdminDTO>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public ActionResult GetAllMeetupsUserWasNotInvited(int userId)
        {
            if (!CheckUserIdFromClaims(userId))
            {
                return Unauthorized();
            }
            var meetupsFromRepo = _meetupsService.GetAllMeetupsUserWasNotInvited(userId);
            if (meetupsFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<IList<MeetupDTO>>(meetupsFromRepo));
        }

        [HttpPut("user/{userId}/me/checkAsistance/{meetUpId}")]
        [ProducesResponseType(typeof(IList<MeetupAdminDTO>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public async Task<ActionResult> CheckAsistanceForMeetup([FromRoute] int userId, int meetUpId)
        {
            if (!CheckUserIdFromClaims(userId))
            {
                return Unauthorized();
            }

            var updateResult = await _userMeetupsService.UpdateCheckIn(userId, meetUpId, true);
            if (!updateResult)
                return BadRequest();

            return NoContent();
        }

        [HttpPost("user/request")]
        public ActionResult CreateRequestForMeetup([FromBody] UserRequestToMeetUpDTO userRequestToMeetUp)
        {
            if (!CheckUserIdFromClaims(userRequestToMeetUp.UserId))
            {
                return Unauthorized();
            }

            var userRequestToMeetUpLogic = _mapper.Map<UserRequestToMeetUp>(userRequestToMeetUp);
            
            var createResult = _requestToMeetUpService.Create(userRequestToMeetUpLogic);
            if (createResult == null)
                return BadRequest();

            return Ok();
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpPost("create/invite")]
        public async Task<ActionResult> CreateInvite([FromBody] UserMeetupDTO userMeetupDTO)
        {            
            var meetupLogic = _mapper.Map<UserMeetUp>(userMeetupDTO);
            var meetupCreated = await _userMeetupsService.Create(meetupLogic);
            if (meetupCreated == null)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpGet("all")]
        [ProducesResponseType(typeof(IList<MeetupAdminDTO>), 200)]
        [ProducesResponseType(typeof(object), 404)]
        public ActionResult GetAllMeetups()
        {
            // agregar paginado
            var meetupsFromRepo = _meetupsService.List();
            if (meetupsFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<IList<MeetupAdminDTO>>(meetupsFromRepo));
        }

        [HttpGet("all/requests")]
        [Authorize(Policy = "OnlyAdmin")]
        public ActionResult GetAllRequests()  
        {
            // agregar paginado
            var meetupsFromRepo = _requestToMeetUpService.List();
            if (meetupsFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<IList<UserRequestToMeetUpDTO>>(meetupsFromRepo));
        }

        [HttpGet("weather/{date}")]
        [Authorize(Policy = "OnlyAdmin")]
        public async Task<ActionResult> GetWeather([FromRoute] DateTimeOffset date)
        {
            var temperature = await _openWeatherService.GetWeatherByDay(date);
            if (temperature.HasValue)
                return Ok(temperature);

            return BadRequest();
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateMeetup([FromBody] MeetupCreateDTO meetupDto)
        {
            try
            {
                var meetupLogic = _mapper.Map<Meetup>(meetupDto);
                var temperatureForMeetupDay = await _openWeatherService.GetWeatherByDay(meetupLogic.MeetupDate);

                if (!temperatureForMeetupDay.HasValue)
                {
                    //loguear error
                    return BadRequest();
                }

                meetupLogic.Temperature = temperatureForMeetupDay.Value;

                var createdMeetUp = await _meetupsService.Create(meetupLogic);
                if (createdMeetUp == null)
                    return UnprocessableEntity();

                return Ok(_mapper.Map<MeetupAdminDTO>(createdMeetUp));
            }
            catch (Exception)
            {
                return BadRequest(); // o Internal Server error
            }
        }

        private bool CheckUserIdFromClaims(int userId)
        {
            ClaimsPrincipal claimsPrincipal = this.User;
            var userIdFromClaims = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
            return userIdFromClaims != null && userIdFromClaims.Value == userId.ToString();
        }
    }
}