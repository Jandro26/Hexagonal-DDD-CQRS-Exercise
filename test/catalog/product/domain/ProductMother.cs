using Hexagonal_Exercise.catalog.product.domain;

namespace Exagonal_exercise.test.catalog.product.domain
{
    public static class ProductMother
    {
        public static Product Create(int? id = null, string name = null)
        => new Product(ProductIdMother.Create(id), ProductNameMother.Create(name));
    }
}
