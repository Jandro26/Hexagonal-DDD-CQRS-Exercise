using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Hexagonal_Exercise.entry_point.catalog.v1.model
{
    public static class RenameProductModelMother
    {
        private static readonly Random _random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string _name = new string(Enumerable.Repeat(chars, 20)
                                                        .Select(s => s[_random.Next(s.Length)]).ToArray());
        public static RenameProductModel Create(string name = null)
        => new RenameProductModel()
        {
            Name = (name == null) ? _name : name
        };
    }
}