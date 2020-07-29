using Hexagonal_Exercise.catalog.core.domain.commandBus;

namespace Hexagonal_Exercise.catalog.product.application.update
{
    public class RenameProductCommand: Command
    {
        public int Id { get; }
        public string Name { get; }

        public RenameProductCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
