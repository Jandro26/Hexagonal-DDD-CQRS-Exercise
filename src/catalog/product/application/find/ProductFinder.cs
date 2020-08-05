using Hexagonal_Exercise.catalog.product.domain;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class ProductFinder
    {
        private readonly DomainProductFinder domainFinder;

        public ProductFinder(ProductRepository productRepository)
        {
            domainFinder = new DomainProductFinder(productRepository);
        }

        public async Task<Product> Execute(ProductId id)
        {
            var product =  await domainFinder.Find(id).ConfigureAwait(false);
            return product;
        }
    }
}
