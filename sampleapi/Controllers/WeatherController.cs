using Microsoft.AspNetCore.Mvc;
using SampleAPI.Models;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private static List<WeatherInfo> weatherList = new()
        {
            new WeatherInfo { Id = 1, City = "Chennai", Temperature = 32 },
            new WeatherInfo { Id = 2, City = "Hyderabad", Temperature = 28 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(weatherList);
        }

        [HttpPost]
        public IActionResult Add(WeatherInfo info)
        {
            weatherList.Add(info);
            return Ok(info);
        }
    }
}
