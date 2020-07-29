using Hexagonal_Exercise.catalog.core.domain;
using Hexagonal_Exercise.catalog.shared.domain.category;

namespace Hexagonal_Exercise.catalog.category.domain
{
    public class Category: AggregateRoot
    {
        public CategoryId Id { get; set; }

        public CategoryName Name { get; set; }

        public Category(CategoryId id, CategoryName name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
