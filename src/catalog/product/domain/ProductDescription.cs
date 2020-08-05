using Hexagonal_Exercise.core.domain;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class ProductDescription: ValueObject
    {
        public string Value { get; }
        public ProductDescription(string description)
        {
            Value = checkDescriptionSize(description);
        }

        private string checkDescriptionSize(string description)
        {
            if(description.Length>50)
                description.Trim().Substring(0, 50);
            return description;
        }
            
    }
}
