using Hexagonal_Exercise.catalog.product.domain;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.infrastructure
{
    public class ProductSQLRepository: IProductRepository
    {
        private readonly string _connectionString;
        private SqlConnection _dbConnection;

        public ProductSQLRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionString"];
        }

        private async Task OpenConnection()
        {
            _dbConnection = new SqlConnection();
            _dbConnection.ConnectionString = _connectionString;
            await _dbConnection.OpenAsync().ConfigureAwait(false);
        }

        private async Task CloseConnection()
        {
            await _dbConnection.CloseAsync().ConfigureAwait(false);
            _dbConnection.Dispose();
        }

        public async Task Add(Product product)
        {
            try
            {
                await OpenConnection().ConfigureAwait(false);
                SqlTransaction sqlTrans = _dbConnection.BeginTransaction();

                var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.Product (Id, Name)  VALUES (@Id, @Name)";
                cmd.Parameters.AddWithValue("@Id", product.Id.Value);
                cmd.Parameters.AddWithValue("@Name", product.Name.Value);
                cmd.Transaction = sqlTrans;
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                sqlTrans.Commit();
            }
            catch (System.Exception)
            {

            }
            finally
            {
                await CloseConnection().ConfigureAwait(false);
            }
        }

        public async Task Modify(ProductId id, Product product)
        {
            try
            {
                await OpenConnection().ConfigureAwait(false);
                SqlTransaction sqlTrans = _dbConnection.BeginTransaction();

                var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = "UPDATE Product SET Name = @Name  WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id.Value);
                cmd.Parameters.AddWithValue("@Name", product.Name.Value);
                cmd.Transaction = sqlTrans;
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                sqlTrans.Commit();
            }
            catch (System.Exception)
            {
            }
            finally
            {
                await CloseConnection().ConfigureAwait(false);
            }
        }

        public async Task Delete(Product product)
        {
            try
            {
                await OpenConnection().ConfigureAwait(false);
                SqlTransaction sqlTrans = _dbConnection.BeginTransaction();

                var cmd = _dbConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM Product WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", product.Id.Value);
                cmd.Transaction = sqlTrans;
                await cmd.ExecuteNonQueryAsync().ConfigureAwait(false);
                sqlTrans.Commit();
            }
            catch (System.Exception ex)
            {
            }
            finally
            {
                await CloseConnection().ConfigureAwait(false);
            }
        }


        public async Task<Product> Get(ProductId id)
        {
            Product result = null;
            try
            {
                await OpenConnection().ConfigureAwait(false);

                var qry = _dbConnection.CreateCommand();
                qry.CommandText = "SELECT * FROM Product WHERE Id = @Id";
                qry.Parameters.AddWithValue("@Id", id.Value);
                var reader = await qry.ExecuteReaderAsync().ConfigureAwait(false);
                
                if (reader.Read())
                    result = new Product(new ProductId(reader.GetInt32(0)), new ProductName(reader.GetString(1)));

            }
            catch (System.Exception)
            {

            }
            finally
            {
                await CloseConnection().ConfigureAwait(false);
            }
            return result;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var result = new List<Product>();
            try
            {
                await OpenConnection().ConfigureAwait(false);

                var qry = _dbConnection.CreateCommand();
                qry.CommandText = "SELECT * FROM Product";
                var reader = await qry.ExecuteReaderAsync().ConfigureAwait(false);

                while (reader.Read())
                {
                    var product = new Product(new ProductId(reader.GetInt32(0)), new ProductName(reader.GetString(1)));
                    result.Add(product);
                }

            }
            catch (System.Exception)
            {

            }
            finally
            {
                await CloseConnection().ConfigureAwait(false);
            }
            return result;
        }
    }
}
