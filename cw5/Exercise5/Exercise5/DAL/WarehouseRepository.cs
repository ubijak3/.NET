using Exercise5.Models;
using Microsoft.Data.SqlClient;

namespace Exercise5.DAL
{
    public class WarehouseRepository : IWarehouseRepository
    {


        private IConfiguration _configuration;

        public WarehouseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> Exists(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"select idWarehouse from warehouse where idWarehouse = @1";
                command.Parameters.AddWithValue("@1", id);
                await connection.OpenAsync();
                if (await command.ExecuteScalarAsync() is not null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
