using Hexagonal_Exercise.core.domain;

namespace Hexagonal_Exercise.catalog.shared.domain.category
{
    public class CategoryId: ValueObject
    {
        public int Value { get; }
        public CategoryId(int id)
        {
            Value = id;
        }
    }
}
