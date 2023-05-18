using Exercise4.Models;
using Exercise4.Models.DTOs;
using Exercise4.Models.DTOs.Exercise4.Models.DTOs;
using Exercise4.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Exercise4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IAnimalRepository _animalRepository;
        public AnimalsController(IConfiguration configuration, IAnimalRepository animalRepository)
        {
            _configuration = configuration;
            _animalRepository = animalRepository;
        }

        [HttpGet]//api/animal/1
        public async Task<IActionResult> GetAnimals(string? orderBy)
        {

            /*if (orderBy is null) orderBy = "name";
            orderBy = orderBy ?? "name";*/
            orderBy ??= "name";

            switch (orderBy)
            {
                case "name": break;
                case "description": break;
                case "category": break;
                case "area": break;
                default: orderBy = "name"; break;
            }

            var animals = new List<Animal>();
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"select * from animal order by {orderBy}";
                await connection.OpenAsync();

                var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    animals.Add(new Animal
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Category = reader.GetString(3),
                        Area = reader.GetString(4)
                    });
                }
            }

            return Ok(animals);
        }

        [HttpPost]
        public async Task<IActionResult> AddAnimal(AddAnimal newAnimal)
        {


            if (await _animalRepository.Exists(newAnimal.ID))
            {
                return Conflict();
            }

            await _animalRepository.Create(new Animal
            {
                ID = newAnimal.ID,
                Name = newAnimal.Name,
                Description = newAnimal.Description,
                Category = newAnimal.Category,
                Area = newAnimal.Area,
            });

            return Created($"/api/animals/{newAnimal.ID}", newAnimal);
        }

        // jesli damy json z id w body to i tak dziala, czy powinno?
        [HttpPut("{index}")]
        public async Task<IActionResult> UpdateAnimal(int index, PutAnimal putAnimal)
        {
            if(await _animalRepository.Exists(index))
            {
                await _animalRepository.Update(new Animal
                    {
                    ID = index,
                    Name = putAnimal.Name,
                    Description = putAnimal.Description,
                    Category = putAnimal.Category,
                    Area = putAnimal.Area,
                });
                return Ok(putAnimal);
            }
            return NotFound();
        }

        [HttpDelete("{index}")]
        public async Task<IActionResult> DeleteAnimal(int index)
        {
            if (!await _animalRepository.Exists(index))
            {
                return NotFound();
            }
            await _animalRepository.Delete(index);
            return Ok();
        }


    }
}
