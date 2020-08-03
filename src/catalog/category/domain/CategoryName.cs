using Hexagonal_Exercise.core.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.category.domain
{
    public class CategoryName: ValueObject
    {
        public string Name { get; }
        public CategoryName(string name)
        {
            Name = name.Trim().Substring(0, 10);
        }
    }
}
