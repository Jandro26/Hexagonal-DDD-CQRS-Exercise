using Exagonal_exercise.test.catalog.product.domain;
using Hexagonal_Exercise.catalog.product.application.update;
using Hexagonal_Exercise.catalog.product.domain;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exagonal_exercise.test.catalog.product.application
{
    public class ProductRenamerShould
    {
        private readonly Mock<ProductRepository> productRepository;
        private readonly ProductRenamer productRenamer;

        public ProductRenamerShould()
        {
            productRepository = new Mock<ProductRepository>();
            productRenamer = new ProductRenamer(productRepository.Object);
        }

        [Fact]
        public async void it_should_rename_product()
        {
            var id = ProductIdMother.Create();
            var product = ProductMother.Create();
            var name_new = ProductNameMother.Create(name:"Rename product");
            productRepository.Setup(x => x.Search(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(product));
            productRepository.Setup(x => x.Modify(It.IsAny<Product>()));

            await productRenamer.Execute(id, name_new).ConfigureAwait(false);

            productRepository.Verify(r => r.Search(id), Times.Once);
            productRepository.Verify(r => r.Modify(product), Times.Once);
            Assert.Equal(name_new, product.Name);
        }

        [Fact]
        public void it_should_fail_when_the_product_no_exist()
        {
            var id = ProductIdMother.Create();
            var product = ProductMother.Create();
            var name_new = ProductNameMother.Create(name: "Rename product");
            productRepository.Setup(x => x.Search(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(null));
            productRepository.Setup(x => x.Modify(It.IsAny<Product>()));

            var task = Assert.ThrowsAsync<Exception>(async () => await productRenamer.Execute(id, name_new).ConfigureAwait(false));

            productRepository.Verify(r => r.Search(id), Times.Once);
            productRepository.Verify(r => r.Modify(product), Times.Never);
            Assert.Equal("Product not exist", task.Result.Message);
        }


    }
}
