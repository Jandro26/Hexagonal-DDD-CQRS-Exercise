using Hexagonal_Exercise.catalog.core.domain;
using Hexagonal_Exercise.catalog.shared.domain.category;

namespace Hexagonal_Exercise.catalog.category.domain
{
    public class Category: AggregateRoot
    {
        public CategoryId Id { get; }

        public CategoryName Name { get; }

        public Category(CategoryId id, CategoryName name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
