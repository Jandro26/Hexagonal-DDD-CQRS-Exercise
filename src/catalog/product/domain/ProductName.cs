using Hexagonal_Exercise.core.domain;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class ProductName: ValueObject
    {
        public string Value { get; }
        public ProductName(string name)
        {
            Value = CheckNameSize(name);
        }

        private string CheckNameSize(string name)
        {
            if(name.Length > 20)
                name.Trim().Substring(0, 20);
            return name;
        }
    }
}
