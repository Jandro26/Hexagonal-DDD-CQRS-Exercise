using Hexagonal_Exercise.core.domain;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class ProductName: ValueObject
    {
        private readonly string _name;
        public string Value { get { return _name; } }
        public ProductName(string name)
        {
            _name = CheckNameSize(name);
        }

        private string CheckNameSize(string name)
        {
            if(name.Length > 20)
                name.Trim().Substring(0, 20);
            return name;
        }
    }
}
