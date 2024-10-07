using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnEVa_API.Fixtures;

namespace OnEVa_API.Controllers
{
#if DEBUG
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly PersonFixture _personFixture;
        private readonly DatasFixture _vehiclesFixture;

        public FixturesController(PersonFixture personFixture, DatasFixture vehiclesFixture)
        {
            _personFixture = personFixture;
            _vehiclesFixture = vehiclesFixture;
        }
        
        [HttpPost]
        public async Task<ActionResult> GenerateFileJsonPersonsFixture(int nbPerson)
        {
            var result = await _personFixture.GenerateJsonFileForCreatePersonsFixtures(nbPerson);
            if (result.Equals("CreatePersonsFixtures.json"))
            {
                return Ok("File Json generated successfully.");
            }
            else
            {
                return StatusCode(500, "Failed to generate file json.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> InsertPersonsFixtures()
        {
            var result = await _personFixture.InsertPersonsFixtures();
            if (result)
            {
                return Ok();
            }
            else { return StatusCode(500); }
        }

        [HttpGet]
        public async Task<ActionResult> GenerateDatabase()
        {
            var res = await _vehiclesFixture.GenerateDatabase();
            if (!string.IsNullOrEmpty(res))
            {
                return Ok(res);
            }
            else { return StatusCode(500); }
        }
    }
#endif
}
