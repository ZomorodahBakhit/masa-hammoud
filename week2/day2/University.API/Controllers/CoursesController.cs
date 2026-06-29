using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using University.API.Filters;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
    {
    [ApiController]
    [TypeFilter (typeof (ApiExceptionFilter))]
    [Route ("api/[controller]")]
    public class CoursesController : ControllerBase
        {
        private readonly ICourseService _service;
        private readonly ILogger<CoursesController> _logger;

        public CoursesController ( ICourseService service, ILogger<CoursesController> logger )
            {
            _service = service;
            _logger = logger;
            }

        [Authorize (Roles = "Teacher,Student")]
        [HttpGet]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public IActionResult GetAll ()
            {
            _logger.LogInformation ("GET /courses called");
            return Ok (_service.GetAll ());
            }

        [Authorize (Roles = "Teacher,Student")]
        [HttpGet ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public IActionResult GetById ( int id )
            {
            _logger.LogInformation ("GET /courses/{Id} called", id);
            return Ok (_service.GetById (id));
            }

        [Authorize (Roles = "Teacher")]
        [HttpPost]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public IActionResult Create ( [FromBody] CreateCourseForm form )
            {
            _logger.LogInformation ("POST /courses called");
            return Ok (_service.Create (form));
            }

        [Authorize (Roles = "Teacher")]
        [HttpPut ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public IActionResult Update ( int id, [FromBody] UpdateCourseForm form )
            {
            _logger.LogInformation ("PUT /courses/{Id} called", id);
            return Ok (_service.Update (id, form));
            }

        [Authorize (Roles = "Teacher")]
        [HttpDelete ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public IActionResult Delete ( int id )
            {
            _logger.LogInformation ("DELETE /courses/{Id} called", id);
            _service.Delete (id);
            return Ok ();
            }
        }
    }