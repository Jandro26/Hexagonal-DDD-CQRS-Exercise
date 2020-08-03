
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hexagonal_Exercise.entry_point.catalog.v1.model
{
    public static class CreateProductModelMother
    {
        private static readonly Random _random = new Random();
        private static readonly int _id = _random.Next(100000);
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string _name = new string(Enumerable.Repeat(chars, 20)
                                                        .Select(s => s[_random.Next(s.Length)]).ToArray());

        public static CreateProductModel Create(int? id = null, string name = null)
        =>new CreateProductModel()
            {
                Id = (id == null) ? _id : id.Value,
                Name = (name == null) ? _name : name
            };
    }
}