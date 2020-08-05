using Hexagonal_Exercise.catalog.core.domain;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.domain
{
    public interface RepositoryBase<TAggregate> where TAggregate: AggregateRoot
    {
        Task Save(TAggregate entity);
        Task Remove(TAggregate entity);
        Task Modify(TAggregate entity);
    }
}
