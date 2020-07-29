using Hexagonal_Exercise.core.domain;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class ProductId: ValueObject
    {
        private readonly int _id; 
        public int Value { get { return _id; } }
        public ProductId(int id)
        {
            _id = id;
        }
    }
}
