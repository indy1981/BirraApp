using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BirrasApp.Repositories
{
    public class EFBaseRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly DbContext _dbContext;

        public EFBaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbContext DbContext
        {
            get
            {
                return _dbContext;
            }
        }

        public virtual async Task<TEntity> Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            await DbContext.AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            DbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual IQueryable<TEntity> Queryable(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>()
            .Where(predicate);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>()
            .Where(predicate).FirstOrDefaultAsync();  // ver si se puede mejorar esto
        }

        public virtual IList<TEntity> List()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public virtual void Update(TEntity entity)
        {
            
            if (entity == null)
            {
                throw new ArgumentNullException("entity is null");
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
