using Exagonal_exercise.test.catalog.product.domain;
using Hexagonal_Exercise.catalog.product.infrastructure;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq;
using Xunit;

namespace Exagonal_exercise.test.catalog.product.infrastructure
{
    public class ProductSQLRepositoryShould
    {
        private readonly ProductSQLRepository productSQLRepository;
        private readonly Mock<IConfiguration> configuration;
        private const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hexagonal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ProductSQLRepositoryShould()
        {
            configuration = new Mock<IConfiguration>();
            configuration.Setup(c => c["ConnectionString"]).Returns(CONNECTION_STRING);
            productSQLRepository = new ProductSQLRepository(configuration.Object);
        }

        [Fact]
        public void It_should_get_a_list_of_product()
        {
            var task = productSQLRepository.GetAll().ConfigureAwait(false);

            var actualProducts = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProducts);
            Assert.NotEmpty(actualProducts);
        }

        [Fact]
        public void It_should_get_an_existing_product()
        {
            var taskpre = productSQLRepository.GetAll().ConfigureAwait(false);
            var existingProduct = (taskpre.GetAwaiter().GetResult()).FirstOrDefault();

            var task = productSQLRepository.Search(existingProduct.Id).ConfigureAwait(false);

            var actualProduct = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProduct);
            Assert.Equal(actualProduct.Id.Value, existingProduct.Id.Value);
        }

        [Fact]
        public void It_should_save_a_new_product()
        {
            var expectedProduct = ProductMother.Create();

            productSQLRepository.Save(expectedProduct).ConfigureAwait(false).GetAwaiter().GetResult();

            var task = productSQLRepository.Search(expectedProduct.Id).ConfigureAwait(false);
            var actualProduct = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProduct);
            Assert.Equal(actualProduct.Id.Value, expectedProduct.Id.Value);
        }

        [Fact]
        public void It_should_rename_an_existing_product()
        {
            var expectedProductName = ProductNameMother.Create();
            var taskpre = productSQLRepository.GetAll().ConfigureAwait(false);
            var existingProduct = (taskpre.GetAwaiter().GetResult()).FirstOrDefault();
            existingProduct.Rename(expectedProductName);

            productSQLRepository.Modify(existingProduct).ConfigureAwait(false).GetAwaiter().GetResult();

            var task = productSQLRepository.Search(existingProduct.Id).ConfigureAwait(false);
            var actualProduct = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProduct);
            Assert.Equal(actualProduct.Name.Value, expectedProductName.Value);
        }

        [Fact]
        public void It_should_delete_an_existing_product()
        {
            var taskpre = productSQLRepository.GetAll().ConfigureAwait(false);
            var existingProduct = (taskpre.GetAwaiter().GetResult()).FirstOrDefault();

            productSQLRepository.Remove(existingProduct).ConfigureAwait(false).GetAwaiter().GetResult();

            var task = productSQLRepository.Search(existingProduct.Id).ConfigureAwait(false);
            var actualProduct = task.GetAwaiter().GetResult();

            Assert.Null(actualProduct);
        }
    }
}
