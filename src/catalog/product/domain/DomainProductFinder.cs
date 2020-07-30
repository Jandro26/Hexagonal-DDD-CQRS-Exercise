using System;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class DomainProductFinder
    {
        private readonly IProductRepository _productRepository;
        public DomainProductFinder(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Find(ProductId id)
        {
            var product = await _productRepository.Get(id).ConfigureAwait(false);
            if (product == null)
                throw new Exception("Product not exist");

            return product;
        }
    }
}
