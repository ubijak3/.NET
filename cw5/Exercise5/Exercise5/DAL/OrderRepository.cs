using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace Exercise5.DAL
{
    public interface IOrderReposiotry
    {
        Task<bool> Exists(int id, int amount, DateTime createdAt);
        Task UpdateStatusAsync(int idProduct, int amount, DateTime createdAt);
    }
    public class OrderRepository : IOrderReposiotry
    {
        private IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration) {
            _configuration = configuration;
        }

        public async Task<bool> Exists(int idProduct, int amount, DateTime createdAt)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"select * from [order] where idProduct = @1 and amount = @2 and fulfilledAt is null and createdAt < @3";
                command.Parameters.AddWithValue("@1", idProduct);
                command.Parameters.AddWithValue("@2", amount);
                var date = createdAt.ToString();
                command.Parameters.AddWithValue("@3", date);
                await connection.OpenAsync();
                if (await command.ExecuteScalarAsync() is not null)
                {
                    return true;
                }
                return false;
            }
        }
        public async Task UpdateStatusAsync(int idProduct, int amount, DateTime createdAt)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"update [order] set fulfilledat = @4 where idProduct = @1 and amount = @2 and fulfilledAt is null and createdAt < @3";
                command.Parameters.AddWithValue("@1", idProduct);
                command.Parameters.AddWithValue("@2", amount);
                var date = createdAt.ToString();
                command.Parameters.AddWithValue("@3", date);
                var dateNow = DateTime.UtcNow;
                command.Parameters.AddWithValue("@4", dateNow);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
