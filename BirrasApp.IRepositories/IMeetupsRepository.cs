using BirrasApp.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BirrasApp.Repositories.Interfaces
{
    public interface IMeetupsRepository
    {
        IList<Meetup> List();

        IQueryable<Meetup> QueryableWithUserMeetups(Expression<Func<Meetup, bool>> predicate);

        Task<Meetup> GetByIdAsync(int id);

        Task<Meetup> GetAsync(Expression<Func<Meetup, bool>> predicate);
        Task<Meetup> Create(Meetup entity);
        void Update(Meetup entity);
    }
}
