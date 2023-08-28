using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WEBTEST_API_PROYECT.Data;
using WEBTEST_API_PROYECT.Models;
using WEBTEST_API_PROYECT.Models.Dto;

namespace WEBTEST_API_PROYECT.Controllers
{
    [Route("api/[controller]")]
    public class WebTestController : Controller
    {

        private readonly ILogger<WebTestController> _logger;
        private readonly ApplicationDbContext _db;

        public WebTestController(ILogger<WebTestController> logger, ApplicationDbContext db) 
        {
            
            _logger = logger;   
            _db = db;
        
        }



        // GET: api/TestWeb
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Indicate that a successful response will have a status code of 200 OK
        public ActionResult<IEnumerable<TestWebDto>> GetTestWebs() // ActionResult indicates the type of data we are returning
        {
            _logger.LogInformation("Obtener todos los test");
            return Ok(TestWebStore.testWebList); // Return an Ok result with the list of test web DTOs

        }


        // GET: api/TestWeb/5
        [HttpGet("id:int", Name = "GetWebTest")] //indica el id como parametro como parte de la ruta
        [ProducesResponseType(StatusCodes.Status200OK)]        // Indicate that a successful response will have a status code of 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Indicate that a bad request response will have a status code of 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]   // Indicate that a not found response will have a status code of 404 Not Found
        public ActionResult<TestWebDto> GetTestWeb(int id)
        {

            // Check if the provided id is valid
            if (id == 0)
            {
                _logger.LogError("Error al obtener el test con Id: " + id);
                return BadRequest(); // Bad request if id is invalid
            }

            // Find the test with the given id
            var test = TestWebStore.testWebList.FirstOrDefault(t => t.Id == id);

            // If the test is not found, return a NotFound result
            if (test == null)
            {
                return NotFound(); // Not found if no test matches the id
            }

            // Return an Ok result with the found test
            return Ok(test); // Ok with the found test

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TestWebDto> CreateTestWeb([FromBody]TestWebDto testDto) /*From Body*/
        {

            // Check if the model state is valid (based on validation attributes)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if(TestWebStore.testWebList.FirstOrDefault(t => t.Name.ToLower() == testDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "el test ya existe");

                return BadRequest(ModelState);
            }

            // Check if the provided testDto is null
            if (testDto == null)
            {
                return BadRequest();
            }
            // Check if the provided testDto has a positive Id (indicating an invalid request)
            else if (testDto.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError); // Return internal server error status
            }

            // Generate a new Id for the testDto
            testDto.Id = TestWebStore.testWebList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            // Add the testDto to the testWebList
            TestWebStore.testWebList.Add(testDto);

            // Return a CreatedAtRoute result with the created testDto and location header
            return CreatedAtRoute("GetWebTest", new { id = testDto.Id }, testDto);
        }


        // PUT: api/TestWeb/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful update will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        public IActionResult UpdateWebTest(int id, [FromBody] TestWebDto testDto)
        {
            // Check if testDto is null or if id in URL does not match id in testDto
            if (testDto == null || id != testDto.Id)
            {
                return BadRequest(ModelState);
            }

            // Find the test to update
            var testWeb = TestWebStore.testWebList.FirstOrDefault(v => v.Id == id);

            // Update properties of the test
            testWeb.Name = testDto.Name;
            testWeb.Pages = testDto.Pages;
            testWeb.SquareMeters = testDto.SquareMeters;

            // Return a NoContent result to indicate successful update
            return NoContent();
        }



        // PATCH: api/TestWeb/{id}
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful update will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        public IActionResult UpdatePartialWebTest(int id, JsonPatchDocument<TestWebDto> patchDto)
        {
            // Check if testDto is null or if id in URL does not match id in testDto
            if (patchDto == null || id == 0)
            {
                return BadRequest(ModelState);
            }

            // Find the test to update
            var testWeb = TestWebStore.testWebList.FirstOrDefault(v => v.Id == id);


            patchDto.ApplyTo(testWeb, ModelState);


            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Return a NoContent result to indicate successful update
            return NoContent();
        }


        // DELETE: api/TestWeb/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful deletion will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]         // Indicate that a not found response will have a status code of 404 Not Found
        public IActionResult DeleteTestWeb(int id)
        {
            // Check if id is invalid
            if (id == 0)
            {
                return BadRequest();
            }

            // Find the test to delete
            var testWeb = TestWebStore.testWebList.FirstOrDefault(v => v.Id == id);

            // If test not found, return a NotFound result
            if (testWeb == null)
            {
                return NotFound(testWeb);
            }

            // Remove the test from the list and return a NoContent result
            TestWebStore.testWebList.Remove(testWeb);
            return NoContent();
        }

    }
    
}
