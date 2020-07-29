using Hexagonal_Exercise.catalog.core.domain.commandBus;

namespace Hexagonal_Exercise.catalog.product.application.delete
{
    public class DeleteProductCommand: Command
    {
        public int Id { get; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
    }
}
