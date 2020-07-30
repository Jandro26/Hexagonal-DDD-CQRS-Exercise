using Hexagonal_Exercise.catalog.product.application.create;
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.eventBus;
using Moq;
using Xunit;

namespace Exagonal_exercise.test.catalog.product.application
{
    public class ProductCreatorShould
    {
        [Fact]
        public async void it_should_create_product()
        {
            var id = new ProductId(2);
            var name = new ProductName("Product name");

            var productRepository = new Mock<IProductRepository>().Object;
            var eventBus = new Mock<IDomainEventBus>().Object;
            var productCreator = new ProductCreator(productRepository, eventBus);
            await productCreator.Execute(id, name).ConfigureAwait(false);
        }

        [Fact]
        public async void it_should_create_product_when_product_name_exceed_max_long()
        {
            var id = new ProductId(2);
            var name = new ProductName("Product name Product name Product name");

            var productRepository = new Mock<IProductRepository>().Object;
            var eventBus = new Mock<IDomainEventBus>().Object;
            var productCreator = new ProductCreator(productRepository, eventBus);
            await productCreator.Execute(id, name).ConfigureAwait(false);
        }
    }
}
