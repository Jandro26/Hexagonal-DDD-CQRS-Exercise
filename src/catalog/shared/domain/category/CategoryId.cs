using Hexagonal_Exercise.core.domain;

namespace Hexagonal_Exercise.catalog.shared.domain.category
{
    public class CategoryId: ValueObject
    {
        public int _id;
        public int Value { get { return _id; } }
        public CategoryId(int id)
        {
            _id = id;
        }
    }
}
