using Hexagonal_Exercise.core.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.domain
{
    public class ProductDescription: ValueObject
    {
        private readonly string _description;
        public string Value { get { return _description; } }
        public ProductDescription(string description)
        {
            _description = checkDescriptionSize(description);
        }

        private string checkDescriptionSize(string description)
        {
            if(description.Length>50)
                description.Trim().Substring(0, 50);
            return description;
        }
            
    }
}
