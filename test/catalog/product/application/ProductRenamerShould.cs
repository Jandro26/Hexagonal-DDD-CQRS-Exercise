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
        private readonly Mock<IProductRepository> _productRepository;
        private readonly ProductRenamer _productRenamer;

        public ProductRenamerShould()
        {
            _productRepository = new Mock<IProductRepository>();
            _productRenamer = new ProductRenamer(_productRepository.Object);
        }

        [Fact]
        public async void it_should_rename_product()
        {
            var id = ProductIdMother.Create();
            var product = ProductMother.Create();
            var name_new = ProductNameMother.Create(name:"Rename product");
            _productRepository.Setup(x => x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(product));
            _productRepository.Setup(x => x.Modify(It.IsAny<ProductId>(), It.IsAny<Product>()));

            await _productRenamer.Execute(id, name_new).ConfigureAwait(false);

            _productRepository.Verify(r => r.Get(id), Times.Once);
            _productRepository.Verify(r => r.Modify(id, product), Times.Once);
            Assert.Equal(name_new, product.Name);
        }

        [Fact]
        public async void it_should_fail_when_the_product_no_exist()
        {
            var id = ProductIdMother.Create();
            var product = ProductMother.Create();
            var name_new = ProductNameMother.Create(name: "Rename product");
            _productRepository.Setup(x => x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(null));
            _productRepository.Setup(x => x.Modify(It.IsAny<ProductId>(), It.IsAny<Product>()));

            var task = Assert.ThrowsAsync<Exception>(async () => await _productRenamer.Execute(id, name_new).ConfigureAwait(false));

            _productRepository.Verify(r => r.Get(id), Times.Once);
            _productRepository.Verify(r => r.Modify(id, product), Times.Never);
            Assert.Equal("Product not exist", task.Result.Message);
        }


    }
}
