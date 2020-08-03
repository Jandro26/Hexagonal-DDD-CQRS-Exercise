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
        private readonly ProductSQLRepository _productSQLRepository;
        private readonly Mock<IConfiguration> _configuration;
        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Hexagonal;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public ProductSQLRepositoryShould()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c["ConnectionString"]).Returns(connectionString);
            _productSQLRepository = new ProductSQLRepository(_configuration.Object);
        }

        [Fact]
        public void It_should_get_a_list_of_product()
        {
            var task = _productSQLRepository.GetAll().ConfigureAwait(false);

            var actualProducts = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProducts);
            Assert.NotEmpty(actualProducts);
        }

        [Fact]
        public void It_should_get_an_existing_product()
        {
            var taskpre = _productSQLRepository.GetAll().ConfigureAwait(false);
            var existingProduct = (taskpre.GetAwaiter().GetResult()).FirstOrDefault();

            var task = _productSQLRepository.Get(existingProduct.Id).ConfigureAwait(false);

            var actualProduct = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProduct);
            Assert.Equal(actualProduct.Id.Value, existingProduct.Id.Value);
        }

        [Fact]
        public void It_should_save_a_new_product()
        {
            var expectedProduct = ProductMother.Create();

            _productSQLRepository.Add(expectedProduct).ConfigureAwait(false).GetAwaiter().GetResult();

            var task = _productSQLRepository.Get(expectedProduct.Id).ConfigureAwait(false);
            var actualProduct = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProduct);
            Assert.Equal(actualProduct.Id.Value, expectedProduct.Id.Value);
        }

        [Fact]
        public void It_should_rename_an_existing_product()
        {
            var expectedProduct = ProductMother.Create();
            var taskpre = _productSQLRepository.GetAll().ConfigureAwait(false);
            var existingProduct = (taskpre.GetAwaiter().GetResult()).FirstOrDefault();

            _productSQLRepository.Modify(existingProduct.Id, expectedProduct).ConfigureAwait(false).GetAwaiter().GetResult();

            var task = _productSQLRepository.Get(existingProduct.Id).ConfigureAwait(false);
            var actualProduct = task.GetAwaiter().GetResult();

            Assert.NotNull(actualProduct);
            Assert.Equal(actualProduct.Name.Value, expectedProduct.Name.Value);
        }

        [Fact]
        public void It_should_delete_an_existing_product()
        {
            var taskpre = _productSQLRepository.GetAll().ConfigureAwait(false);
            var existingProduct = (taskpre.GetAwaiter().GetResult()).FirstOrDefault();

            _productSQLRepository.Delete(existingProduct).ConfigureAwait(false).GetAwaiter().GetResult();

            var task = _productSQLRepository.Get(existingProduct.Id).ConfigureAwait(false);
            var actualProduct = task.GetAwaiter().GetResult();

            Assert.Null(actualProduct);
        }
    }
}
