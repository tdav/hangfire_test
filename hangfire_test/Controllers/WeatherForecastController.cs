using Microsoft.AspNetCore.Mvc;

namespace hangfire_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public string Get()
        {
            return DateTime.Now.ToString();
        }
    }
}