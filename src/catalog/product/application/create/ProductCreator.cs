using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.eventBus;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.create
{
    public class ProductCreator
    {
        private readonly ProductRepository productRepository;
        private readonly DomainEventBus eventBus;

        public ProductCreator(ProductRepository productRepository, DomainEventBus eventBus)
        {
            this.productRepository = productRepository;
            this.eventBus = eventBus;
        }

        public async Task Execute(ProductId id, ProductName name)
        {
            var product = Product.Create(id, name);

            await productRepository.Save(product).ConfigureAwait(false);
            eventBus.Publish(product.GetDomainEvents());
        }
    }
}
 
