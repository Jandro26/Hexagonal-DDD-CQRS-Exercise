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
        private readonly Mock<ProductRepository> productRepository;
        private readonly ProductDeleter productDeleter;

        public ProductDeleterShould()
        {
            productRepository = new Mock<ProductRepository>();
            productDeleter = new ProductDeleter(productRepository.Object);
        }

        [Fact]
        public async void It_should_delete_product()
        {
            var product = ProductMother.Create();
            var id = ProductIdMother.Create();

            productRepository.Setup(x => x.Search(It.IsAny<ProductId>())).Returns(Task.FromResult(product));
            productRepository.Setup(x => x.Remove(It.IsAny<Product>()));

            await productDeleter.Execute(id).ConfigureAwait(false);

            productRepository.Verify(r => r.Search(id), Times.Once);
            productRepository.Verify(r => r.Remove(product), Times.Once);
        }

        [Fact]
        public void It_should_faild_when_product_not_exist()
        {
            var product = ProductMother.Create();
            var id = ProductIdMother.Create();

            productRepository.Setup(x => x.Search(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(null));
            productRepository.Setup(x => x.Remove(It.IsAny<Product>()));


            var task =  Assert.ThrowsAsync<Exception>(async()=>await productDeleter.Execute(id).ConfigureAwait(false));

            Assert.Equal("Product not exist", task.Result.Message);
            productRepository.Verify(r => r.Search(id), Times.Once);
            productRepository.Verify(r => r.Remove(product), Times.Never);
        }
    }
}
