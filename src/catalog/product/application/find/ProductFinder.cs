using Hexagonal_Exercise.catalog.product.domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class ProductFinder
    {
        private readonly IProductRepository _productRepository;
        private readonly DomainProductFinder _domainFinder;

        public ProductFinder(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _domainFinder = new DomainProductFinder(productRepository);
        }

        public async Task<Product> Execute(ProductId id)
        {
            var product =  await _domainFinder.Find(id).ConfigureAwait(false);
            return product;
        }
    }
}
