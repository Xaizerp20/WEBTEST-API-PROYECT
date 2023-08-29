using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WEBTEST_API_PROYECT.Data;
using WEBTEST_API_PROYECT.Models;
using WEBTEST_API_PROYECT.Models.Dto;
using WEBTEST_API_PROYECT.Repository.IRepository;

namespace WEBTEST_API_PROYECT.Controllers
{
    [Route("api/[controller]")]
    public class NumberWebTestController : Controller
    {

        private readonly ILogger<NumberWebTestController> _logger;
        //private readonly ApplicationDbContext _db;
        private readonly ITestWebRepository _testWebRepo;
        private readonly INumberTestWebRepository _numberTestWebRepo;
        private readonly IMapper _mapper;
        protected APIResponse _response;


        public NumberWebTestController(ILogger<NumberWebTestController> logger, ITestWebRepository testWebRepo, INumberTestWebRepository numberTestWebRepo, IMapper mapper) 
        {
            
            _logger = logger;
            _testWebRepo = testWebRepo;
            _numberTestWebRepo = numberTestWebRepo;
            //_db = db;
            _mapper = mapper;
            _response = new();
        }   



        // GET: api/TestWeb
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)] // Indicate that a successful response will have a status code of 200 OK
        public async Task<ActionResult<APIResponse>> GetNumberTestWebs() // ActionResult indicates the type of data we are returning
        {
            try
            {
                _logger.LogInformation("Obtener todos numeros de los test");

                IEnumerable<NumberTestWeb> NumerotestWebList = await _numberTestWebRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<NumberTestWeb>>(NumerotestWebList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response); // Return an Ok result with the list of test web DTOs

            }

            catch(Exception ex)
            {
                _response.isSucessfull = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return _response;

        }


        // GET: api/TestWeb/5
        [HttpGet("id:int", Name = "GetNumberTestWeb")] //indica el id como parametro como parte de la ruta
        [ProducesResponseType(StatusCodes.Status200OK)]        // Indicate that a successful response will have a status code of 200 OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // Indicate that a bad request response will have a status code of 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]   // Indicate that a not found response will have a status code of 404 Not Found
        public async Task<ActionResult<APIResponse>> GetNumberTestWeb(int id)
        {


            try
            {
                // Check if the provided id is valid
                if (id == 0)
                {
                    _logger.LogError("Error al obtener el numero test con Id: " + id);
                    _response.isSucessfull = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response); // Bad request if id is invalid
                }

                // Find the test with the given id
                // var test = TestWebStore.testWebList.FirstOrDefault(t => t.Id == id);
                var Numbertest = await _numberTestWebRepo.Get(t => t.TestWebNo == id);

                // If the test is not found, return a NotFound result
                if (Numbertest == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.isSucessfull = false;
                    return NotFound(_response); // Not found if no test matches the id
                }

                _response.Result = _mapper.Map<NumberTestWebDto>(Numbertest);
                
                // Return an Ok result with the found test
                return Ok(_response); // Ok with the found test
            }
            catch(Exception ex)
            {
                _response.isSucessfull = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return _response;

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateNumberTestWeb([FromBody] NumberTestWebCreateDto CreateDto) /*From Body*/
        {

            try
            {

                // Check if the model state is valid (based on validation attributes)
                if (!ModelState.IsValid)
                {
                   // _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(ModelState);
                }

                if (await _numberTestWebRepo.Get(t => t.TestWebNo == CreateDto.TestWebNo) != null)
                {
                    ModelState.AddModelError("NameExist", "el test ya existe");

                    return BadRequest(ModelState);
                }

                if(await _testWebRepo.Get(v => v.Id == CreateDto.TestWebId) == null)
                {
                    ModelState.AddModelError("ClaveForanea", "el id de la web  no existe");

                    return BadRequest(ModelState);
                }

                // Check if the provided testDto is null
                if (CreateDto == null)
                {
                    return BadRequest();
                }
                // Check if the provided testDto has a positive Id (indicating an invalid request)


                NumberTestWeb model = _mapper.Map<NumberTestWeb>(CreateDto);

                model.CreationDate = DateTime.Now;
                model.UpdatingTime = DateTime.Now;
                await _numberTestWebRepo.Create(model); //add model to database (insert)
                _response.Result = model;
                _response.StatusCode = HttpStatusCode.Created;
                                                  //await _db.SaveChangesAsync(); //save changes



                // Generate a new Id for the testDto
                //testDto.Id = TestWebStore.testWebList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
                // Add the testDto to the testWebList
                //TestWebStore.testWebList.Add(testDto);


                // Return a CreatedAtRoute result with the created testDto and location header
                return CreatedAtRoute("GetNumberTestWeb", new { id = model.TestWebNo }, _response);
            }
            catch(Exception ex)
            {
                _response.isSucessfull = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return _response;
        }


        // PUT: api/TestWeb/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful update will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        public async Task<IActionResult> UpdateWebTest(int id, [FromBody] NumberTestWebUpdateDto updateDto)
        {


            // Check if testDto is null or if id in URL does not match id in testDto
            if (updateDto == null || id != updateDto.TestWebNo)
            {
                _response.isSucessfull = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(ModelState);
            }

            if(await _testWebRepo.Get(v => v.Id == updateDto.TestWebId) == null)
            {
                ModelState.AddModelError("ClaveForanea", "el id de la web  no existe");

                return BadRequest(ModelState);
            }

            //// Find the test to update
            //var testWeb = TestWebStore.testWebList.FirstOrDefault(v => v.Id == id);

            //// Update properties of the test
            //testWeb.Name = testDto.Name;
            //testWeb.Pages = testDto.Pages;
            //testWeb.SquareMeters = testDto.SquareMeters;

            NumberTestWeb model = _mapper.Map<NumberTestWeb>(updateDto);

            await _numberTestWebRepo.Update(model);
            //await _db.SaveChangesAsync();

            // Return a NoContent result to indicate successful update

            _response.StatusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }


       


        // DELETE: api/TestWeb/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        // Indicate that a successful deletion will have a status code of 204 No Content
        [ProducesResponseType(StatusCodes.Status400BadRequest)]       // Indicate that a bad request response will have a status code of 400 Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)]         // Indicate that a not found response will have a status code of 404 Not Found
        public async Task<IActionResult> DeleteNumberTestWeb(int id)
        {

            try
            {
                // Check if id is invalid
                if (id == 0)
                {
                    _response.isSucessfull = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                // Find the test to delete
                var numberTestWeb = await _numberTestWebRepo.Get(v => v.TestWebNo == id);

                // If test not found, return a NotFound result
                if (numberTestWeb == null)
                {
                    _response.isSucessfull = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                // Remove the test from the list and return a NoContent result

                await _numberTestWebRepo.Delete(numberTestWeb);
                //await _db.SaveChangesAsync();

                // TestWebStore.testWebList.Remove(testWeb);
              
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.isSucessfull = false;
                _response.ErrorMessage = new List<string> { ex.ToString() };
            }

            return BadRequest(_response); 


        }

    }
    
}
