using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.domain;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.application.update
{
    public class ProductRenamer
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductFinder _productFinder;
        public ProductRenamer(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _productFinder = new ProductFinder(productRepository);
        }

        public async Task Execute(ProductId id, ProductName newName)
        {
            var product = await _productFinder.Execute(id).ConfigureAwait(false);
            product.Rename(newName);
            await _productRepository.Modify(id, product).ConfigureAwait(false);
        } 
    }
}
