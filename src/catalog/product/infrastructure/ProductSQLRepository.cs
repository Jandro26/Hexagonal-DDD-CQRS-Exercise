using Hexagonal_Exercise.catalog.product.domain;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.infrastructure
{
    public class ProductSQLRepository: IProductRepository
    {
        private readonly SqlConnection _dbConnection;

        public ProductSQLRepository(IConfiguration configuration)
        {
            _dbConnection = new SqlConnection();
            _dbConnection.ConnectionString = configuration["ConnectionString"];
        }

        public async Task Add(Product product)
        {

                await _dbConnection.OpenAsync().ConfigureAwait(false);
                SqlTransaction sqlTrans = _dbConnection.BeginTransaction();

                var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Product (Id, Name)  VALUES (@Id, @Name)";
                cmd.Parameters.AddWithValue("@Id", product.Id.Value);
                cmd.Parameters.AddWithValue("@Name", product.Name.Value);
                cmd.Transaction = sqlTrans;
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                sqlTrans.Commit();

                await _dbConnection.CloseAsync().ConfigureAwait(false);
                _dbConnection.Dispose();
        }

        public async Task Modify(ProductId id, Product product)
        {

            await _dbConnection.OpenAsync().ConfigureAwait(false);
            SqlTransaction sqlTrans = _dbConnection.BeginTransaction();

            var cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "UPDATE Product SET Name = @Name  WHERE Id = @Id";
            cmd.Parameters.AddWithValue("@Id", id.Value);
            cmd.Parameters.AddWithValue("@Name", product.Name.Value);
            cmd.Transaction = sqlTrans;
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            sqlTrans.Commit();

            await _dbConnection.CloseAsync().ConfigureAwait(false);
            _dbConnection.Dispose();
        }

        public async Task Delete(Product product)
        {

            await _dbConnection.OpenAsync().ConfigureAwait(false);
            await _dbConnection.BeginTransactionAsync().ConfigureAwait(false);
            SqlTransaction sqlTrans = _dbConnection.BeginTransaction();

            var cmd = _dbConnection.CreateCommand();
            cmd.CommandText = "DELETE * FROM Product WHERE Id = @Id)";
            cmd.Parameters.AddWithValue("@Id", product.Id.Value);
            cmd.Transaction = sqlTrans;
            await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
            sqlTrans.Commit();

            await _dbConnection.CloseAsync().ConfigureAwait(false);
            _dbConnection.Dispose();
        }


        public async Task<Product> Get(ProductId id)
        {
            await _dbConnection.OpenAsync().ConfigureAwait(false);

            var qry = _dbConnection.CreateCommand();
            qry.CommandText = "SELECT * FROM Product WHERE Id = @Id";
            qry.Parameters.AddWithValue("@Id", id.Value);
            var reader = await qry.ExecuteReaderAsync().ConfigureAwait(false); ;

            if (reader == null) return null;
            Product result = new Product(new ProductId(reader.GetInt32(0)), new ProductName(reader.GetString(1)));

            await _dbConnection.CloseAsync().ConfigureAwait(false);
            _dbConnection.Dispose();

            return result;
        }
    }
}
