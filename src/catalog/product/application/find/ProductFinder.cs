using Hexagonal_Exercise.catalog.product.domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class ProductFinder
    {
        private readonly IProductRepository _productRepository;
        public ProductFinder (IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> Execute(ProductId id)
        {
            var product =  await _productRepository.Get(id).ConfigureAwait(false);
            if (product == null)
                throw new Exception("Product not exist");

            return product;
        }
    }
}
