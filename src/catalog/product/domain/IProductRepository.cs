using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public interface IProductRepository
    {
        public Task Add(Product product);

        public Task Modify(ProductId id, Product product);

        public Task Delete(Product product);

        public Task<Product> Get(ProductId id);

        public Task<IEnumerable<Product>> GetAll();

    }
}
