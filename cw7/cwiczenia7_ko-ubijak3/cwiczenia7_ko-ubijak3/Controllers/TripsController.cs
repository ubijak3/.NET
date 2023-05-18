using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cwiczenia7_ko_ubijak3.DAL;
using Microsoft.EntityFrameworkCore;
using cwiczenia7_ko_ubijak3.DTO_s;
using System.Net.Sockets;

namespace cwiczenia7_ko_ubijak3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {

        private readonly _2019sbdContext _context;
        public TripsController(_2019sbdContext _2019SbdContext)
        {
            _context = _2019SbdContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var trips = await _context.Trips.Select(e => new
            {
                name = e.Name,
                description = e.Description,
                dateFrom = e.DateFrom,
                dateTo = e.DateTo,
                maxPeople = e.MaxPeople,
                countries = e.IdCountries.Select(e => new
                {
                    name = e.Name
                }),
                clients = e.ClientTrips.Select(e => new
                {
                    firstName = e.IdClientNavigation.FirstName,
                    lastName = e.IdClientNavigation.LastName,
                })
            })
            .ToListAsync();
            return Ok(trips);
        }

        [HttpDelete("api/clients/{idClient}")]
        public async Task<IActionResult> Remove(int idClient) {
            var client = await _context.Clients.FirstOrDefaultAsync(e => e.IdClient == idClient);
            if (client == null) return NotFound();
            var clientTrips = await _context.ClientTrips.Where(e => e.IdClient == idClient).ToListAsync();
            if(clientTrips.Any())
            {
                return BadRequest("Klient posiada przypisana wycieczke");
            }
            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("api/trips/{idTrip}/clients")]
        public async Task<IActionResult> AddClientToTrip(ClientToTripRequest clientToTripRequest)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(e => e.Pesel == clientToTripRequest.pesel);
            if (client == null)
            {
                await _context.Clients.AddAsync(new Client
                {
                    IdClient =_context.Clients.Max(e => e.IdClient) + 1,
                    FirstName = clientToTripRequest.firstName,
                    LastName = clientToTripRequest.lastName,
                    Email = clientToTripRequest.email,
                    Telephone = clientToTripRequest.telephone,
                    Pesel = clientToTripRequest.pesel,
                });
                await _context.SaveChangesAsync();
            }
            var clientToAdd = await _context.Clients.FirstOrDefaultAsync(e => e.Pesel == clientToTripRequest.pesel);
            var clientTrip = await _context.ClientTrips.FirstOrDefaultAsync(e => e.IdClient == clientToAdd.IdClient);
            if (clientTrip != null) return BadRequest("Kilent jest juz zapisany na ta wycieczke");
            var wycieczka = await _context.Trips.FirstOrDefaultAsync(e => e.IdTrip == clientToTripRequest.tripId);
            if (wycieczka == null) return BadRequest("Wycieczka nie istnieje");

            

            await _context.ClientTrips.AddAsync(new ClientTrip
            {
                IdClient = clientToAdd.IdClient,
                IdTrip = clientToTripRequest.tripId,
                RegisteredAt = DateTime.UtcNow,
                PaymentDate = clientToTripRequest?.paymentDate,
            });
            await _context.SaveChangesAsync();
            return Ok("Klient dodany");
        }
    }
}
