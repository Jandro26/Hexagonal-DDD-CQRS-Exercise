using Hexagonal_Exercise.catalog.core.domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.domain
{
    public interface IRepositoryBase<TAggregate> where TAggregate: AggregateRoot
    {
        Task<TAggregate> GetAsync(Expression<Func<TAggregate, bool>> predicate);
        Task<IEnumerable<TAggregate>> GetListAsync(Expression<Func<TAggregate, bool>> predicate);
        void Add(TAggregate entity);
        void AddRange(IEnumerable<TAggregate> entities);
        void Remove(TAggregate entity);
        void RemoveRange(IEnumerable<TAggregate> entities);
        void Update(TAggregate entity);
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
