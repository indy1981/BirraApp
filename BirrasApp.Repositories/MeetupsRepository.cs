using BirrasApp.Repositories.Interfaces;
using BirrasApp.Repositories.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BirrasApp.Repositories
{
    public class MeetupsRepository : EFBaseRepository<Meetup, int>, IMeetupsRepository
    {
        public MeetupsRepository(BirrasAppContext context) : base(context) { }

        public override IList<Meetup> List()
        {
            return _dbContext.Set<Meetup>().Include(x => x.UserMeetUps).ToList();
        }

        public IQueryable<Meetup> QueryableWithUserMeetups(Expression<Func<Meetup, bool>> predicate)
        {
            return _dbContext.Set<Meetup>().Include(x => x.UserMeetUps).Where(predicate);
        }

        public override async Task<Meetup> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Meetup>().Include(x => x.UserMeetUps).FirstOrDefaultAsync( meetup => meetup.Id == id );
        }

    }
}
