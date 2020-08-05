using Hexagonal_Exercise.core.domain;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class ProductId: ValueObject
    {
        public int Value { get ; }
        public ProductId(int id)
        {
            Value = id;
        }
    }
}
