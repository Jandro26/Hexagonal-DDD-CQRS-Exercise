using Hexagonal_Exercise.catalog.core.domain;
using Hexagonal_Exercise.catalog.shared.domain.category;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class Product: AggregateRoot
    {
        public ProductId Id { get; set; }
        public ProductName Name { get; set; }
        public CategoryId CategoryId { get; set; }
        public ProductDescription Description {get; set;}

        public Product(ProductId id, ProductName name)
        {
            this.Id = id;
            this.Name = name;
        }

        public static Product Create(ProductId id, ProductName name)
        {
            var product = new Product(id, name);
            product.AddDomainEvent(new ProductCreatedDomainEvent());
            return product;
        }

        public void Rename(ProductName newName)
        {
            this.Name = newName;
        }


    }
}
