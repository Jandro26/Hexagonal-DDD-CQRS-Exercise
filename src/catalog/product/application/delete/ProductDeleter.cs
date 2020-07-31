using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.domain;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.delete
{
    public class ProductDeleter
    {
        private readonly IProductRepository _productRepository;
        private readonly DomainProductFinder _productFinder;

        public ProductDeleter(IProductRepository productRepository)
        {
            _productFinder = new DomainProductFinder(productRepository);
            _productRepository = productRepository;
        }

        public async Task Execute(ProductId id)
        {
            var product = await _productFinder.Find(id).ConfigureAwait(false);
            await _productRepository.Delete(product).ConfigureAwait(false);
        }
    }
}
