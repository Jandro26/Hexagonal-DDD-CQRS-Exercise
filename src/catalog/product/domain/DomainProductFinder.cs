using System;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class DomainProductFinder
    {
        private readonly ProductRepository productRepository;
        public DomainProductFinder(ProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<Product> Find(ProductId id)
        {
            var product = await productRepository.Search(id).ConfigureAwait(false);
            if (product == null)
                throw new Exception("Product not exist");

            return product;
        }
    }
}
