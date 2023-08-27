using Microsoft.AspNetCore.Mvc;
using WEBTEST_API_PROYECT.Data;
using WEBTEST_API_PROYECT.Models;
using WEBTEST_API_PROYECT.Models.Dto;

namespace WEBTEST_API_PROYECT.Controllers
{
    [Route("api/[controller]")]
    public class WebTestController : Controller
    {
        // GET: api/TestWeb
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Indicate that a successful response will have a status code of 200 OK
        public ActionResult<IEnumerable<TestWebDto>> GetTestWebs() // ActionResult indicates the type of data we are returning
        {

            return Ok(TestWebStore.testWebList); // Return an Ok result with the list of test web DTOs

        }


        // GET: api/TestWeb/5
        [HttpGet("id:int")] //indica el id como parametro como parte de la ruta
        [ProducesResponseType(StatusCodes.Status200OK)]        // Indicate that a successful response will have a status code of 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Indicate that a bad request response will have a status code of 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]   // Indicate that a not found response will have a status code of 404 Not Found
        public ActionResult<TestWebDto> GetTestWeb(int id)
        {

            // Check if the provided id is valid
            if (id == 0)
            {
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TestWebDto> CreateTestWeb([FromBody]TestWebDto testDto) /*From Body*/
        {

        }


    }
}
