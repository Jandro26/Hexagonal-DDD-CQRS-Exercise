using Hexagonal_Exercise.catalog.product.domain;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.catalog.product.infrastructure
{
    public class ProductSQLRepository: ProductRepository
    {
        private readonly string connectionString;
        private SqlConnection dbConnection;

        public ProductSQLRepository(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionString"];
        }

        private async Task OpenConnection()
        {
            dbConnection = new SqlConnection();
            dbConnection.ConnectionString = connectionString;
            await dbConnection.OpenAsync().ConfigureAwait(false);
        }

        private async Task CloseConnection()
        {
            await dbConnection.CloseAsync().ConfigureAwait(false);
            dbConnection.Dispose();
        }

        public async Task Save(Product product)
        {
            try
            {
                await OpenConnection().ConfigureAwait(false);
                SqlTransaction sqlTrans = dbConnection.BeginTransaction();

                var cmd = dbConnection.CreateCommand();
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

        public async Task Modify(Product product)
        {
            try
            {
                await OpenConnection().ConfigureAwait(false);
                SqlTransaction sqlTrans = dbConnection.BeginTransaction();

                var cmd = dbConnection.CreateCommand();
                cmd.CommandText = "UPDATE Product SET Name = @Name  WHERE Id = @Id";
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

        public async Task Remove(Product product)
        {
            try
            {
                await OpenConnection().ConfigureAwait(false);
                SqlTransaction sqlTrans = dbConnection.BeginTransaction();

                var cmd = dbConnection.CreateCommand();
                cmd.CommandText = "DELETE FROM Product WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", product.Id.Value);
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


        public async Task<Product> Search(ProductId id)
        {
            Product result = null;
            try
            {
                await OpenConnection().ConfigureAwait(false);

                var qry = dbConnection.CreateCommand();
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

                var qry = dbConnection.CreateCommand();
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
