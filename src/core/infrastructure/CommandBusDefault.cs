using Hexagonal_Exercise.catalog.core.domain.commandBus;
using Hexagonal_Exercise.core.application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.core.infrastructure
{
    public class CommandBusDefault : CommandDispacher
    {
        private readonly IServiceProvider serviceProvider;
        public CommandBusDefault(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task Dispatch<TCommand>(TCommand command, CancellationToken cancellationToken) where TCommand : Command
        {
            var handler = serviceProvider.GetRequiredService<CommandHandler<TCommand>>();
            if (handler == null) throw new NullReferenceException($"handler of {nameof(TCommand)}");

            return handler.Dispatch(command, cancellationToken);
        }
    }
}
