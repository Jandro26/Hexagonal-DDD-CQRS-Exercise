using Hexagonal_Exercise.catalog.product.application.create;
using Hexagonal_Exercise.catalog.product.application.find;
using Hexagonal_Exercise.catalog.product.application.update;
using Hexagonal_Exercise.catalog.product.domain;
using Hexagonal_Exercise.core.domain.eventBus;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exagonal_exercise.test
{
    public class Product_test
    {
        [Fact]
        public async void Check_product_creator_succed()
        {
            var id = new ProductId(2);
            var name = new ProductName("Product name");

            var productRepository = new Mock<IProductRepository>().Object;
            var eventBus = new Mock<IDomainEventBus>().Object;
            var productCreator = new ProductCreator(productRepository, eventBus);
            await productCreator.Execute(id, name).ConfigureAwait(false);
        }

        [Fact]
        public async void Check_product_creator_succed_product_name_max_long()
        {
            var id = new ProductId(2);
            var name = new ProductName("Product name Product name Product name");

            var productRepository = new Mock<IProductRepository>().Object;
            var eventBus = new Mock<IDomainEventBus>().Object;
            var productCreator = new ProductCreator(productRepository, eventBus);
            await productCreator.Execute(id, name).ConfigureAwait(false);
        }

        [Fact]
        public async void Check_product_rename_succed()
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

        [Fact]
        public async void Check_product_finder_faild_product_not_find()
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
