using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemperatureLibrary;

namespace TempAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : Controller
    {
        private readonly TemperatureRepository _repo;

        public TemperatureController(TemperatureRepository temperatureRepository)
        {
            _repo = temperatureRepository;
        }

        // GETALL
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Temperature>> Get()
        {
            if (_repo.GetAllTemperatures().Count == 0)
            {
                return NotFound("No items in the list");
            }
            return Ok(_repo.GetAllTemperatures());

        }
        // GET
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Temperature> Get(int id)
        {
            Temperature temp = _repo.GetTemperature(id);
            if (temp == null)
            {
                return NotFound("No such item, id " + id);
            }
            return Ok(temp);
        }

        // POST / Add
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public ActionResult<Temperature> Post([FromBody] Temperature value)
        {
            if (value == null)
            {
                return BadRequest("Temperature data is null."); // Return 400 if data is missing or invalid
            }


            Temperature addedTemperature = _repo.AddTemperature(value);


            // If temperature is successfully added, return 201 and the created object
            return CreatedAtAction(nameof(Get), new { id = addedTemperature.Id }, addedTemperature);
        }

        // Put / Update
        [ProducesResponseType(StatusCodes.Status200OK)]  // For successful update
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  // For invalid input
        [ProducesResponseType(StatusCodes.Status404NotFound)]  // When Temperature is not found
        [HttpPut("{id}")]
        public ActionResult<Temperature> Put(int id, [FromBody] Temperature value)
        {
            throw new NotImplementedException();
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
