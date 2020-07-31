using Exagonal_exercise.test.catalog.product.domain;
using Hexagonal_Exercise.catalog.product.application.update;
using Hexagonal_Exercise.catalog.product.domain;
using Moq;
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
            var name_new = ProductNameMother.Create("Rename product");
            _productRepository.Setup(x => x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult<Product>(product));
            _productRepository.Setup(x => x.Modify(It.IsAny<ProductId>(), It.IsAny<Product>()));

            await _productRenamer.Execute(id, name_new).ConfigureAwait(false);

            _productRepository.Verify(r => r.Get(id), Times.Once);
            _productRepository.Verify(r => r.Modify(id, product), Times.Once);
            Assert.Equal(name_new, product.Name);
        }


    }
}
