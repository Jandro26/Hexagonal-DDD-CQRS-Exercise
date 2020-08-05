using Exagonal_exercise.test.catalog.product.domain;
using Hexagonal_Exercise.catalog.product.application.create;
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.eventBus;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Exagonal_exercise.test.catalog.product.application
{
    public class ProductCreatorShould
    {
        private readonly Mock<ProductRepository> productRepository;
        private readonly Mock<DomainEventBus> eventBus;
        private readonly ProductCreator productCreator; 

        public ProductCreatorShould()
        {
            productRepository = new Mock<ProductRepository>();
            eventBus = new Mock<DomainEventBus>();
            productCreator = new ProductCreator(productRepository.Object, eventBus.Object);
        }

        [Fact]
        public async void it_should_create_product()
        {
            var id = ProductIdMother.Create();
            var name = ProductNameMother.Create();
            productRepository.Setup(x => x.Save(It.IsAny<Product>()));

            await productCreator.Execute(id, name).ConfigureAwait(false);

            productRepository.Verify(r => r.Save(It.IsAny<Product>()), Times.Once);
            eventBus.Verify(r => r.Publish(It.IsAny<IEnumerable<DomainEvent>>()), Times.Once);

        }

        [Fact]
        public async void it_should_create_product_when_product_name_exceed_max_long()
        {
            var id = ProductIdMother.Create();
            var name = ProductNameMother.Create(name:"Product name Product name Product name");

            await productCreator.Execute(id, name).ConfigureAwait(false);

            productRepository.Verify(r => r.Save(It.IsAny<Product>()), Times.Once);
            eventBus.Verify(r => r.Publish(It.IsAny<IEnumerable<DomainEvent>>()), Times.Once);
        }
    }
}
