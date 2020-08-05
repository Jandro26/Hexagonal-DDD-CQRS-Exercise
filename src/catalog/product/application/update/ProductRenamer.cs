using Hexagonal_Exercise.catalog.product.domain;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.update
{
    public class ProductRenamer
    {
        private readonly ProductRepository productRepository;
        private readonly DomainProductFinder productFinder;
        public ProductRenamer(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
            productFinder = new DomainProductFinder(productRepository);
        }

        public async Task Execute(ProductId id, ProductName newName)
        {
            var product = await productFinder.Find(id).ConfigureAwait(false);
            product.Rename(newName);
            await productRepository.Modify(product).ConfigureAwait(false);
        } 
    }
}
