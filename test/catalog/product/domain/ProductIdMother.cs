using Hexagonal_Exercise.catalog.product.domain;
using System;

namespace Exagonal_exercise.test.catalog.product.domain
{
    public static class ProductIdMother
    {
        const int MAX_LENGHT_ID = 100000;
        private static readonly int randomId = new Random().Next(MAX_LENGHT_ID);

        public static ProductId Create(int? id = null)
        => new ProductId((id == null)? randomId : id.Value);
    }
}
