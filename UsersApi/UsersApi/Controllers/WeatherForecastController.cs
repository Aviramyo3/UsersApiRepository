using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UsersApi.Configs;
using UsersApi.Utils;

namespace UsersApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {   
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly AppSettings appSettings;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<AppSettings> options)
        {
            appSettings = options.Value;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("validateToken")]
        [AllowAnonymous] // Allows unauthenticated requests to validate the token
        public async Task<IActionResult> ValidateToken([FromBody] string idToken)
        {
            try
            {
                //idToken =  await FirebaseUtils.GetFirebaseIdToken();
                var decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
                // Token is valid, continue with the desired logic
                // Access user information from `decodedToken`
                return Ok(idToken);
            }
            catch (FirebaseAuthException)
            {
                // Token is invalid or expired
                return Unauthorized();
            }
        }
    }
}