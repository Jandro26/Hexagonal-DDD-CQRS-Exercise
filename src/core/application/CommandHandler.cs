using Hexagonal_Exercise.catalog.core.domain.commandBus;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.application
{
    public interface CommandHandler<in TCommand> where TCommand: Command
    {
        Task Dispatch(TCommand command, CancellationToken cancellationToken);
    }
}
