using Microsoft.AspNetCore.Mvc;
using ProjetoChat.Models;

namespace ProjetoChat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast forecast)
        {
            if (forecast == null)
            {
                return BadRequest("Previs�o inv�lida.");
            }

            _logger.LogInformation("Nova previs�o recebida: {Date}, {Temp}C, {Summary}",
                forecast.Date, forecast.TemperatureC, forecast.Summary);

            // Aqui voc� pode adicionar l�gica para salvar em banco, etc.
            return CreatedAtAction(nameof(Get), new { }, forecast);
        }
    }
}
