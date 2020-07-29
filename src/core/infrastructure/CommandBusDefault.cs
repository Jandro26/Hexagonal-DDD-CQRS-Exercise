using Hexagonal_Exercise.catalog.core.domain.commandBus;
using Hexagonal_Exercise.core.application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.core.infrastructure
{
    public class CommandBusDefault : ICommandDispacher
    {
        private readonly IServiceProvider _serviceProvider;
        public CommandBusDefault(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : Command
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            if (handler == null) throw new NullReferenceException($"handler of {nameof(TCommand)}");

            return handler.Dispatch(command, cancellationToken);
        }
    }
}
