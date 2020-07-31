using Exagonal_exercise.test.catalog.product.domain;
using Hexagonal_Exercise.catalog.product.application.delete;
using Hexagonal_Exercise.catalog.product.domain;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exagonal_exercise.test.catalog.product.application
{
    public class ProductDeleterShould
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductDeleter _productDeleter;

        public ProductDeleterShould()
        {
            _productRepository = new Mock<IProductRepository>();
            _productDeleter = new ProductDeleter(_productRepository.Object);
        }

        [Fact]
        public async void It_should_delete_product()
        {
            var product = ProductMother.Create();
            var id = ProductIdMother.Create();

            _productRepository.Setup(x => x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult(product));
            _productRepository.Setup(x => x.Delete(It.IsAny<Product>()));

            await _productDeleter.Execute(id).ConfigureAwait(false);

            _productRepository.Verify(r => r.Get(id), Times.Once);
            _productRepository.Verify(r => r.Delete(product), Times.Once);
        }

        [Fact]
        public void It_should_faild_when_product_not_exist()
        {
            var product = ProductMother.Create();
            var id = ProductIdMother.Create();

            _productRepository.Setup(x => x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(null));
            _productRepository.Setup(x => x.Delete(It.IsAny<Product>()));


            var task =  Assert.ThrowsAsync<Exception>(async()=>await _productDeleter.Execute(id).ConfigureAwait(false));

            Assert.Equal("Product not exist", task.Result.Message);
            _productRepository.Verify(r => r.Get(id), Times.Once);
            _productRepository.Verify(r => r.Delete(product), Times.Never);
        }
    }
}
