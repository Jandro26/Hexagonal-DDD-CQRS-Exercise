using Hexagonal_Exercise.catalog.product.domain;
using System;

namespace Exagonal_exercise.test.catalog.product.domain
{
    public static class ProductIdMother
    {
        private static readonly int _id = new Random().Next(100000);

        public static ProductId Create(int? id = null)
        => new ProductId((id == null)?_id : id.Value);
    }
}
