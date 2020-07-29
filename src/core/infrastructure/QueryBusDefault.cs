using Hexagonal_Exercise.core.domain.queryBus;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Hexagonal_Exercise.core.application;

namespace Hexagonal_Exercise.core.infrastructure
{
    public class QueryBusDefault : IQueryDispacher
    {
        private readonly IServiceProvider _serviceProvider;
        public QueryBusDefault(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : Query where TResult : QueryResult
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            if (handler == null)
                throw new NullReferenceException($"handler of {nameof(TQuery)} which returning {nameof(TResult)}");

            return handler.Dispatch(query, cancellationToken);
        }
    }
}
