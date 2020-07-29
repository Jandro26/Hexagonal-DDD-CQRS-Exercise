using Hexagonal_Exercise.catalog.core.domain.commandBus;

namespace Hexagonal_Exercise.catalog.product.application.create
{
    public class CreateProductCommand: Command
    {
        public int Id { get; }
        public string Name { get; }

        public CreateProductCommand(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}