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
    public class RepositoryBase<TAggregate> : domain.RepositoryBase<TAggregate> where TAggregate : AggregateRoot
    {

        private readonly DataDbContext dbContext;
        protected readonly DbSet<TAggregate> dbSets;

        public RepositoryBase(DataDbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException();
            dbSets = dbContext.Set<TAggregate>();
        }


        public void Dispose()
        {
            dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(TAggregate entity)
        {
            dbSets.Add(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Remove(TAggregate entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached) dbSets.Attach(entity);
            dbSets.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task Modify(TAggregate entity)
        {
            dbSets.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TAggregate>> SearchAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await dbSets.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TAggregate> GetAsync(Expression<Func<TAggregate, bool>> predicate)
        {
            return await dbSets.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }
    }
}