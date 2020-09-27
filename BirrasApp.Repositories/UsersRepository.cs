using System.Threading.Tasks;
using BirrasApp.Repositories.Interfaces;
using BirrasApp.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace BirrasApp.Repositories
{
    public class UsersRepository : EFBaseRepository<User, int>, IUsersRepository
    {
        public UsersRepository(BirrasAppContext context) : base(context)
        {
        }

        public async Task<User> GetByUsername(string username)
        {
            return await GetAsync(user => user.Username == username);
        }

    }
}
