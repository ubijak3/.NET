using Exercise5.DAL;
using Exercise5.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Exercise5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        public readonly IWarehouseRepository _warhouseRepository;
        private IConfiguration _configuration;
        public readonly IOrderReposiotry _orderRepository;
        private object idMaxPW;
        private object idMaxOrder;
        private decimal price;

        public WarehousesController(IWarehouseRepository warehouseRepository, IConfiguration configuration, IOrderReposiotry orderReposiotry)
        {
            _warhouseRepository = warehouseRepository;
            _configuration = configuration;
            _orderRepository = orderReposiotry;
        }

        [HttpPost] 
            public async Task<IActionResult> Create(OrderPOST newOrder)
            {
                if (newOrder.amount <= 0)
                {
                    return BadRequest("amount mniejsza lub rowna 0");
                } 

                if (!await _warhouseRepository.Exists(newOrder.idWarehouse)){
                    return NotFound("Hurtownia nie istnieje");
                }
      
                if(!await _orderRepository.Exists(newOrder.idProduct, newOrder.amount, newOrder.createdAt))
                {
                return NotFound("brak zlecenia produktu");
                }

                using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"select Max(idOrder) from [order]";
                    await connection.OpenAsync();
                    if ((idMaxOrder = await command.ExecuteScalarAsync()) is null)
                    {
                        idMaxOrder = 1;
                    }
                    else
                    {
                        idMaxOrder = (int)idMaxOrder;
                    }
                }
                using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"select price from product where idProduct = @1";
                    command.Parameters.AddWithValue("@1", newOrder.idProduct);
                    await connection.OpenAsync();
                    price = (decimal)await command.ExecuteScalarAsync();
                    
                }

                await _orderRepository.UpdateStatusAsync(newOrder.idProduct,newOrder.amount, newOrder.createdAt);

                using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"insert into Product_warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) values (@2, @3, @4, @5, @6, @7)";
                    command.Parameters.AddWithValue("@2", newOrder.idWarehouse);
                    command.Parameters.AddWithValue("@3", newOrder.idProduct);
                    command.Parameters.AddWithValue("@4", idMaxOrder);
                    command.Parameters.AddWithValue("@5", newOrder.amount);
                    command.Parameters.AddWithValue("@6", newOrder.amount*price);
                    command.Parameters.AddWithValue("@7", DateTime.UtcNow);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return Ok();
            }
        }
    }

    
