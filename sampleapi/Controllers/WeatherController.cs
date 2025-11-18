using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        // In-memory storage for simplicity
        private static readonly List<WeatherInfo> weatherList = new()
        {
            new WeatherInfo { Id = 1, City = "Chennai", Temperature = 32 },
            new WeatherInfo { Id = 2, City = "Hyderabad", Temperature = 28 }
        };

        // GET: /api/weather
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(weatherList);
        }

        // GET: /
        [HttpGet("/")]
        public IActionResult Root()
        {
            return Ok("Weather API is running!");
        }

        // POST: /api/weather
        [HttpPost]
        public IActionResult Add([FromBody] WeatherInfo info)
        {
            if (info == null || string.IsNullOrWhiteSpace(info.City))
            {
                return BadRequest("City and temperature must be provided.");
            }

            // Generate Id automatically
            info.Id = weatherList.Any() ? weatherList.Max(w => w.Id) + 1 : 1;
            weatherList.Add(info);

            return CreatedAtAction(nameof(GetAll), new { id = info.Id }, info);
        }

        // GET: /api/weather/{id}
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var weather = weatherList.FirstOrDefault(w => w.Id == id);
            if (weather == null)
                return NotFound($"Weather info with Id {id} not found.");

            return Ok(weather);
        }
    }
}
