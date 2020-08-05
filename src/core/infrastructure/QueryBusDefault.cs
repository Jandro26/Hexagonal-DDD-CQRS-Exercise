using Hexagonal_Exercise.core.domain.queryBus;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Hexagonal_Exercise.core.application;

namespace Hexagonal_Exercise.core.infrastructure
{
    public class QueryBusDefault : QueryDispacher
    {
        private readonly IServiceProvider serviceProvider;
        public QueryBusDefault(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task<TResult> Dispatch<TQuery, TResult>(TQuery query, CancellationToken cancellationToken) where TQuery : Query where TResult : QueryResult
        {
            var handler = serviceProvider.GetRequiredService<QueryHandler<TQuery, TResult>>();
            if (handler == null)
                throw new NullReferenceException($"handler of {nameof(TQuery)} which returning {nameof(TResult)}");

            return handler.Dispatch(query, cancellationToken);
        }
    }
}
