using Exercise4.Models;
using System.Data.SqlClient;

namespace Exercise4.Repository
{
    public interface IAnimalRepository
    {
        Task<bool> Exists(int id);
        Task Create(Animal animal);
        Task Delete(int id);
        Task Update(Animal animal);
    }

    public class AnimalRepository : IAnimalRepository
    {
        private readonly IConfiguration _configuration;
        public AnimalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Exists(int id)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"select id from animal where id = @1";
                command.Parameters.AddWithValue("@1", id);
                await connection.OpenAsync();
                if (await command.ExecuteScalarAsync() is not null)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task Create(Animal animal)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"insert into animal (id, name, description, category, area) values (@1, @2, @3, @4, @5)";
                command.Parameters.AddWithValue("@1", animal.ID);
                command.Parameters.AddWithValue("@2", animal.Name);
                command.Parameters.AddWithValue("@3", animal.Description);
                command.Parameters.AddWithValue("@4", animal.Category);
                command.Parameters.AddWithValue("@5", animal.Area);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task Update(Animal animal)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"update animal set name = @2, description = @3, category = @4, area = @5 where id = @1";
                command.Parameters.AddWithValue("@1", animal.ID);
                command.Parameters.AddWithValue("@2", animal.Name);
                command.Parameters.AddWithValue("@3", animal.Description);
                command.Parameters.AddWithValue("@4", animal.Category);
                command.Parameters.AddWithValue("@5", animal.Area);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
        public async Task Delete(int index)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"delete from animal where id = @1";
                command.Parameters.AddWithValue("@1", index);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
