using Hexagonal_Exercise.catalog.core.domain;
using Hexagonal_Exercise.core.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.infrastructure.entityFramework
{
    public class RepositoryBase<TAggregate> : IRepositoryBase<TAggregate> where TAggregate : AggregateRoot
    {

        private readonly DataDbContext _dbContext;
        protected readonly DbSet<TAggregate> _dbSets;

        public RepositoryBase(DataDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException();
            _dbSets = _dbContext.Set<TAggregate>();
        }


        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<TAggregate> GetAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await _dbSets.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TAggregate>> GetListAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await _dbSets.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IQueryable<TAggregate> Queryable(Expression<Func<TAggregate, bool>> predicate)
        {
            return _dbSets.Where(predicate);
        }

        public void Add(TAggregate entity)
        {
            _dbSets.Add(entity);
        }

        public void AddRange(IEnumerable<TAggregate> entities)
        {
            _dbSets.AddRange(entities);
        }

        public void Remove(TAggregate entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached) _dbSets.Attach(entity);

            _dbSets.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TAggregate> entities)
        {
            foreach (var entity in entities)
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached) _dbSets.Attach(entity);

                _dbSets.Remove(entity);
            }
        }

        public void Update(TAggregate entity)
        {
            _dbSets.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}