using Microsoft.AspNetCore.Mvc;
using SampleMultitenantAutofac.Repository;

namespace SampleMultitenantAutofac.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private TestDbContext _context;
        public WeatherForecastController(TestDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<Person> Get()
        {
            return _context.People.ToList();
        }

        [HttpPost]
        public IActionResult Post(long id, string personName)
        {
            _context.People.Add(new Person(id, personName));

            _context.SaveChanges();

            return Ok(id);

        }
    }
}