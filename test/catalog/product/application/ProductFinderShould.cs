
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
        [Fact]
        public async void it_should_faild_when_product_not_exist()
        {
            var id = new ProductId(2);
            var productRepository = new Mock<IProductRepository>();
                productRepository.Setup(x=>x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(null));
            var productFinder = new ProductFinder(productRepository.Object);

            var task = Assert.ThrowsAsync<Exception>(async () => await productFinder.Execute(id).ConfigureAwait(false));

            Assert.Equal("Product not exist", task.Result.Message);
        }
    }
}
