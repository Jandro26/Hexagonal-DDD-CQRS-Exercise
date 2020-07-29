using Hexagonal_Exercise.core.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.category.domain
{
    public class CategoryName: ValueObject
    {
        private string _name;
        public string Value { get {return _name; } }
        public CategoryName(string name)
        {
            _name = name.Trim().Substring(0, 10);
        }
    }
}
