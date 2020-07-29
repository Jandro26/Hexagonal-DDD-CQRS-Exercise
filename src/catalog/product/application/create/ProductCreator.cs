using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.eventBus;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.create
{
    public class ProductCreator
    {
        private readonly IProductRepository _productRepository;
        private readonly IDomainEventBus _eventBus;

        public ProductCreator(IProductRepository productRepository, IDomainEventBus eventBus)
        {
            _productRepository = productRepository;
            _eventBus = eventBus;
        }

        public async Task Execute(ProductId id, ProductName name)
        {
            var product = Product.Create(id, name);

            await _productRepository.Add(product).ConfigureAwait(false);
            _eventBus.Publish(product.DomainEvents);
        }
    }
}
 
