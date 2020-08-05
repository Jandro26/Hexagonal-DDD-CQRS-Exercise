using Hexagonal_Exercise.core.domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public interface ProductRepository: RepositoryBase<Product>
    {
        public Task<Product> Search(ProductId id);
        public Task<IEnumerable<Product>> GetAll();

    }
}
