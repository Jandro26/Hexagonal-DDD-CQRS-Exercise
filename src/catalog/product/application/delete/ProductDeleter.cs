using Hexagonal_Exercise.catalog.product.domain;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.delete
{
    public class ProductDeleter
    {
        private readonly ProductRepository productRepository;
        private readonly DomainProductFinder productFinder;

        public ProductDeleter(ProductRepository productRepository)
        {
            productFinder = new DomainProductFinder(productRepository);
            this.productRepository = productRepository;
        }

        public async Task Execute(ProductId id)
        {
            var product = await productFinder.Find(id).ConfigureAwait(false);
            await productRepository.Remove(product).ConfigureAwait(false);
        }
    }
}
