using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.queryBus;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class FindProductQueryResult: QueryResult
    {
        public int Id { get; }
        public string Name { get; }
        public int? CategoryId { get; }
        public string Description { get; }

        public FindProductQueryResult(Product product)
        {
            Id = product.Id.Value;
            Name = product.Name.Value;
            CategoryId = product.CategoryId?.Value;
            Description = product.Description?.Value;
        }
    }
}

