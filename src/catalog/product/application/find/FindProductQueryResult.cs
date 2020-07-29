using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.queryBus;

namespace Hexagonal_Exercise.catalog.product.application.find
{
    public class FindProductQueryResult: QueryResult
    {
        private readonly int _id;
        private readonly string _name;
        private readonly int _categoryId;
        private readonly string _description;

        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public int CategoryId { get { return _categoryId; } }
        public string Description { get { return _description; } }

        public FindProductQueryResult(Product product)
        {
            _id = product.Id.Value;
            _name = product.Name.Value;
            _categoryId = product.CategoryId.Value;
            _description = product.Description.Value;
        }
    }
}

