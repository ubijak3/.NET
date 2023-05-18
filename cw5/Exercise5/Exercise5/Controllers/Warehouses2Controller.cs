using Exercise5.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Exercise5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Warehouses2Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterNewProduct(OrderPOST dto)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = connection.CreateCommand();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "AddProductToWarehouse";
                command.Parameters.AddWithValue("@IdProduct", dto.idProduct);
                command.Parameters.AddWithValue("@IdWarehouse", dto.idWarehouse);
                command.Parameters.AddWithValue("@Amount", dto.amount);
                command.Parameters.AddWithValue("@CreatedAt", dto.createdAt);
                await connection.OpenAsync();
                return Created("", Convert.ToString(await command.ExecuteScalarAsync()));
            }
        }
    }
}
