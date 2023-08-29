using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IMapper _mapper;

        public WebTestController(ILogger<WebTestController> logger, ApplicationDbContext db, IMapper mapper) 
        {
            
            _logger = logger;   
            _db = db;
            _mapper = mapper;
        
        }



        // GET: api/TestWeb
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Indicate that a successful response will have a status code of 200 OK
        public async Task<ActionResult<IEnumerable<TestWebDto>>> GetTestWebs() // ActionResult indicates the type of data we are returning
        {
            _logger.LogInformation("Obtener todos los test");

            IEnumerable<TestWeb> testWebList = await _db.TestWebs.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<TestWebDto>>(testWebList)); // Return an Ok result with the list of test web DTOs

        }


        // GET: api/TestWeb/5
        [HttpGet("id:int", Name = "GetWebTest")] //indica el id como parametro como parte de la ruta
        [ProducesResponseType(StatusCodes.Status200OK)]        // Indicate that a successful response will have a status code of 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Indicate that a bad request response will have a status code of 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]   // Indicate that a not found response will have a status code of 404 Not Found
        public async Task<ActionResult<TestWebDto>> GetTestWeb(int id)
        {

            // Check if the provided id is valid
            if (id == 0)
            {
                _logger.LogError("Error al obtener el test con Id: " + id);
                return BadRequest(); // Bad request if id is invalid
            }

            // Find the test with the given id
            // var test = TestWebStore.testWebList.FirstOrDefault(t => t.Id == id);
            var test = await _db.TestWebs.FirstOrDefaultAsync(t => t.Id == id);

            // If the test is not found, return a NotFound result
            if (test == null)
            {
                return NotFound(); // Not found if no test matches the id
            }

            // Return an Ok result with the found test
            return Ok(_mapper.Map<TestWebDto>(test)); // Ok with the found test

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TestWebCreateDto>> CreateTestWeb([FromBody] TestWebCreateDto CreateDto) /*From Body*/
        {

            // Check if the model state is valid (based on validation attributes)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if(await _db.TestWebs.FirstOrDefaultAsync(t => t.Name.ToLower() == CreateDto.Name.ToLower()) != null)
            {
                ModelState.AddModelError("NameExist", "el test ya existe");

                return BadRequest(ModelState);
            }

            // Check if the provided testDto is null
            if (CreateDto == null)
            {
                return BadRequest();
            }
            // Check if the provided testDto has a positive Id (indicating an invalid request)


            TestWeb model = _mapper.Map<TestWeb>(CreateDto);


            await _db.TestWebs.AddAsync(model); //add model to database (insert)
            await _db.SaveChangesAsync(); //save changes



                // Generate a new Id for the testDto
            //testDto.Id = TestWebStore.testWebList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
                // Add the testDto to the testWebList
            //TestWebStore.testWebList.Add(testDto);


            // Return a CreatedAtRoute result with the created testDto and location header
            return CreatedAtRoute("GetWebTest", new { id = model.Id }, model);
        }


        // PUT: api/TestWeb/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful update will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        public async Task<IActionResult> UpdateWebTest(int id, [FromBody] TestWebUpdateDto updateDto)
        {
            // Check if testDto is null or if id in URL does not match id in testDto
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest(ModelState);
            }

            //// Find the test to update
            //var testWeb = TestWebStore.testWebList.FirstOrDefault(v => v.Id == id);

            //// Update properties of the test
            //testWeb.Name = testDto.Name;
            //testWeb.Pages = testDto.Pages;
            //testWeb.SquareMeters = testDto.SquareMeters;

            TestWeb model = _mapper.Map<TestWeb>(updateDto);

            _db.TestWebs.Update(model);
            await _db.SaveChangesAsync();

            // Return a NoContent result to indicate successful update
            return NoContent();
        }


        //TODO: BUG CORRECT
        // PATCH: api/TestWeb/{id}
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful update will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        public async Task<IActionResult> UpdatePartialWebTest(int id, JsonPatchDocument<TestWebUpdateDto> patchDto)
        {
            // Check if testDto is null or if id in URL does not match id in testDto
            if (patchDto == null || id == 0)
            {
                return BadRequest(ModelState);
            }

            // Find the test to update
           // var testWeb = TestWebStore.testWebList.FirstOrDefault(v => v.Id == id);

            var testWeb = await _db.TestWebs.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);


            TestWebUpdateDto testDto = _mapper.Map<TestWebUpdateDto>(patchDto);


            if(testWeb == null)
            {
                return BadRequest();
            }


            patchDto.ApplyTo(testDto, ModelState);


            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TestWeb model = _mapper.Map<TestWeb>(testDto);

            _db.TestWebs.Update(model);
            await _db.SaveChangesAsync();

            // Return a NoContent result to indicate successful update
            return NoContent();
        }


        // DELETE: api/TestWeb/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful deletion will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]         // Indicate that a not found response will have a status code of 404 Not Found
        public async Task<IActionResult> DeleteTestWeb(int id)
        {
            // Check if id is invalid
            if (id == 0)
            {
                return BadRequest();
            }

            // Find the test to delete
            var testWeb = await _db.TestWebs.FirstOrDefaultAsync(v => v.Id == id);

            // If test not found, return a NotFound result
            if (testWeb == null)
            {
                return NotFound(testWeb);
            }

            // Remove the test from the list and return a NoContent result
             
            _db.TestWebs.Remove(testWeb);
            await _db.SaveChangesAsync();

           // TestWebStore.testWebList.Remove(testWeb);
            return NoContent();
        }

    }
    
}
