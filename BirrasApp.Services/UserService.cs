using AutoMapper;
using BirrasApp.Repositories.Interfaces;
using BirrasApp.Services.Interfaces;
using BirrasApp.Services.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repo = BirrasApp.Repositories.Models;

namespace BirrasApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public UserService(IUsersRepository usersRepository, IMapper mapper, ISecurityService securityService)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _securityService = securityService;
        }

        public async Task<User> Register(User user)
        {
            var userToPersist = _mapper.Map<Repo.User>(user);
            _securityService.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            userToPersist.PasswordHash = passwordHash;
            userToPersist.PasswordSalt = passwordSalt;

            var userCreated = await _usersRepository.Create(userToPersist);
            if (userCreated == null)
            {
                // Add logs here
                return null;
            }

            return _mapper.Map<User>(userCreated);
        }

        public async Task<User> GetByUsername(string username)
        {
            var userFromRepo = await _usersRepository.GetByUsername(username);
            if (userFromRepo == null)
            {
                return null;
            }

            return _mapper.Map<User>(userFromRepo);
        }

        public async Task<User> Login(User user)
        {
            var userFromDatabase = await _usersRepository.GetByUsername(user.Username);
            if (userFromDatabase == null)
            {
                return null;
            }

            if(!_securityService.VerifyPasswordHash(user.Password, userFromDatabase.PasswordHash, userFromDatabase.PasswordSalt))
            {
                return null;
            }

            return _mapper.Map<User>(userFromDatabase);
        }

        public IList<User> GetAllNonAdminUsers()
        {
            var usersNonAdmin = _usersRepository.Queryable(user => user.UserRol == Repo.Enums.UsersRoles.User).ToList();
            if (usersNonAdmin == null)
                return null;

            return _mapper.Map<IList<User>>(usersNonAdmin);
        }

    }
}
