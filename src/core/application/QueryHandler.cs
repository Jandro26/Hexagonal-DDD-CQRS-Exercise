
using Hexagonal_Exercise.core.domain.queryBus;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.application
{
    public interface QueryHandler<in TQuery, TResult> 
        where TQuery: Query 
        where TResult : QueryResult
    {
        Task<TResult> Dispatch(TQuery query, CancellationToken cancellationToken);
    }
}
