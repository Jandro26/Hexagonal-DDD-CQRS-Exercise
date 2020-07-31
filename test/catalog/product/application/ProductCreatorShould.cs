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
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<IDomainEventBus> _eventBus;
        private readonly ProductCreator _productCreator; 

        public ProductCreatorShould()
        {
            _productRepository = new Mock<IProductRepository>();
            _eventBus = new Mock<IDomainEventBus>();
            _productCreator = new ProductCreator(_productRepository.Object, _eventBus.Object);
        }

        [Fact]
        public async void it_should_create_product()
        {
            var id = ProductIdMother.Create();
            var name = ProductNameMother.Create();
            _productRepository.Setup(x => x.Add(It.IsAny<Product>()));

            await _productCreator.Execute(id, name).ConfigureAwait(false);

            _productRepository.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
            _eventBus.Verify(r => r.Publish(It.IsAny<IEnumerable<DomainEvent>>()), Times.Once);

        }

        [Fact]
        public async void it_should_create_product_when_product_name_exceed_max_long()
        {
            var id = ProductIdMother.Create();
            var name = ProductNameMother.Create(name:"Product name Product name Product name");

            await _productCreator.Execute(id, name).ConfigureAwait(false);

            _productRepository.Verify(r => r.Add(It.IsAny<Product>()), Times.Once);
            _eventBus.Verify(r => r.Publish(It.IsAny<IEnumerable<DomainEvent>>()), Times.Once);
        }
    }
}
