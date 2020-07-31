using Hexagonal_Exercise.catalog.product.domain;
using System;
using System.Linq;

namespace Exagonal_exercise.test.catalog.product.domain
{
    public static class ProductNameMother
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly Random _random = new Random();
        private static readonly string _name = new string(Enumerable.Repeat(chars, 20)
                                                        .Select(s => s[_random.Next(s.Length)]).ToArray());
        public static ProductName Create(string name = null)
            => new ProductName((name==null)?_name:name);
    }
}
