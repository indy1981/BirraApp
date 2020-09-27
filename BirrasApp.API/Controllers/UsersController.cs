using AutoMapper;
using BirrasApp.DTOs;
using BirrasApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Logic = BirrasApp.Services.Models;

namespace BirrasApp.API.Controllers
{
    [Route("api/users")]
    [ApiController]    
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO userForLogin)
        {
            var userLogic = _mapper.Map<Logic.User>(userForLogin);
            var userData = await _userService.Login(userLogic);

            if (userData == null)
            {
                return Unauthorized("Usuario o Password inválidos");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                new Claim(ClaimTypes.Name, userData.Username),
                new Claim(ClaimTypes.Role, userData.UserRol.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            
            var userLoginResponse = _mapper.Map<UserLoginResponse>(userData);
            userLoginResponse.AccessToken = tokenHandler.WriteToken(token);
            return base.Ok(userLoginResponse);
        }


        [HttpPost("register")]           
        public async Task<ActionResult> RegisterUser([FromBody] UserDTO userForRegisterDto)
        {
            if (await _userService.GetByUsername(userForRegisterDto.Username) != null)
            {
                return BadRequest("Username already exists");
            }
           
            var userLogic = _mapper.Map<Logic.User>(userForRegisterDto);
                        
            userLogic.UserRol = Logic.Enums.UsersRoles.User;
            var createdUSer = _userService.Register(userLogic);
            if (createdUSer == null)
            {
                return BadRequest("Could not create user");
            }

            return StatusCode(201);
        }

        [HttpGet("allUsers")]
        public ActionResult GetAllNonAdminUsers()
        {
            // mejorar
            return Ok(_userService.GetAllNonAdminUsers());
        }
    }
}
