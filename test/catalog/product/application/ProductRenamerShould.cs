using Hexagonal_Exercise.catalog.product.application.create;
using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.application.update;
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.eventBus;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exagonal_exercise.test.catalog.product.application
{
    public class ProductRenamerShould
    {
        [Fact]
        public async void it_should_rename_product()
        {
            var id = new ProductId(2);
            var name = new ProductName("Product name");
            var name_new = new ProductName("Product Rename");

            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(x => x.Get(It.IsAny<ProductId>())).Returns(Task.FromResult(new Product(id, name)));
            productRepository.Setup(x => x.Modify(It.IsAny<ProductId>(), It.IsAny<Product>()));
            var productRenamer = new ProductRenamer(productRepository.Object);
            await productRenamer.Execute(id, name_new).ConfigureAwait(false);
        }
    }
}
