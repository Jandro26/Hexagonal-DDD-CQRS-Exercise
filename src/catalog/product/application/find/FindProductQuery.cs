using Hexagonal_Exercise.core.domain.queryBus;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class FindProductQuery: Query
    {
        public int Id { get; }
        public FindProductQuery(int id)
        {
            Id = id;
        }
    }
} 
