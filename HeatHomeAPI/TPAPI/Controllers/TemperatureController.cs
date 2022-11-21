using Microsoft.AspNetCore.Mvc;
using TPAPI.Domain.Entities;
using TPAPI.Domain.Interfaces.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TPAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : Controller
    {
        private readonly ITemperatureService _temperatureService;

        public TemperatureController(ITemperatureService temperatureService)
        {
            _temperatureService = temperatureService;
        }

        // GET: api/<TemperatureController>
        [HttpGet("/all")]
        public IEnumerable<Temperature> GetAll()
        {
            return _temperatureService.GetAll();
        }

        // GET api/<TemperatureController>/5
        [HttpGet("{id}")]
        public Temperature Get(Guid id)
        {
            return _temperatureService.Get(id);
        }

        // POST api/<TemperatureController>
        [HttpPost]
        public void Post(string value)
        {
            Console.WriteLine("From Post:" + value);
        }

        // PUT api/<TemperatureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TemperatureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
