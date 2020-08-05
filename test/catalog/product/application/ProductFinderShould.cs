
using Exagonal_exercise.test.catalog.product.domain;
using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.domain;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exagonal_exercise.test.catalog.product.application
{
    public class ProductFinderShould
    {
        private readonly Mock<ProductRepository> productRepository;
        private readonly ProductFinder productFinder;

        public ProductFinderShould()
        {
            productRepository = new Mock<ProductRepository>();
            productFinder = new ProductFinder(productRepository.Object);
        }

        [Fact]
        public async void It_should_return_a_product()
        {
            var id = ProductIdMother.Create();
            var expectedProduct = ProductMother.Create();
            productRepository.Setup(x => x.Search(It.IsAny<ProductId>())).Returns(Task.FromResult(expectedProduct));

            var actualProduct = await productFinder.Execute(id).ConfigureAwait(false);

            productRepository.Verify(r => r.Search(id), Times.Once);
            Assert.Same(expectedProduct, actualProduct);
        }

        [Fact]
        public void it_should_faild_when_a_product_not_exist()
        {
            var id = ProductIdMother.Create();
            productRepository.Setup(x=>x.Search(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(null));

            var task = Assert.ThrowsAsync<Exception>(async () => await productFinder.Execute(id).ConfigureAwait(false));

            productRepository.Verify(r => r.Search(id), Times.Once);
            Assert.Equal("Product not exist", task.Result.Message);
        }
    }
}
