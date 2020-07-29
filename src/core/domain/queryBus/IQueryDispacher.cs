using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.domain.queryBus
{
    public interface IQueryDispacher
    {
        Task<TResult> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery: Query where TResult: QueryResult;
    }
}
