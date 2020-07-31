
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
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductFinder _productFinder;

        public ProductFinderShould()
        {
            _productRepository = new Mock<IProductRepository>();
            _productFinder = new ProductFinder(_productRepository.Object);
        }

        [Fact]
        public async void It_should_return_a_product()
        {
            var id = ProductIdMother.Create();
            var expectedProduct = ProductMother.Create();
            _productRepository.Setup(x => x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult(expectedProduct));

            var actualProduct = await _productFinder.Execute(id).ConfigureAwait(false);

            _productRepository.Verify(r => r.Get(id), Times.Once);
            Assert.Same(expectedProduct, actualProduct);
        }

        [Fact]
        public void it_should_faild_when_a_product_not_exist()
        {
            var id = ProductIdMother.Create();
            _productRepository.Setup(x=>x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(null));

            var task = Assert.ThrowsAsync<Exception>(async () => await _productFinder.Execute(id).ConfigureAwait(false));

            _productRepository.Verify(r => r.Get(id), Times.Once);
            Assert.Equal("Product not exist", task.Result.Message);
        }
    }
}
