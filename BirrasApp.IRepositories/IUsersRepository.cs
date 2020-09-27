using BirrasApp.Repositories.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BirrasApp.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User> GetByUsername(string username);

        Task<User> Create(User entity);

        IQueryable<User> Queryable(Expression<Func<User, bool>> predicate);
    }
}
