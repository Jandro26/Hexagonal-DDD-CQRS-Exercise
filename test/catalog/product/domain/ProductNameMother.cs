using Hexagonal_Exercise.catalog.product.domain;
using System;
using System.Linq;

namespace Exagonal_exercise.test.catalog.product.domain
{
    public static class ProductNameMother
    {
        const string ALLOWED_CHARACTERS = "ACDEFGHIJKLMNOPQRSTUVWXYZ";
        const int NAME_MAX_LENGTH = 20;
        private static readonly Random random = new Random();
        private static readonly string randomName = new string(Enumerable.Repeat(ALLOWED_CHARACTERS, NAME_MAX_LENGTH)
                                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        public static ProductName Create(string name = null)
            => new ProductName((name==null)? randomName : name);
    }
}
