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
    public class StudentsController : ControllerBase
        {
        private readonly IStudentService _service;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController ( IStudentService service, ILogger<StudentsController> logger )
            {
            _service = service;
            _logger = logger;
            }

        [Authorize (Roles = "Teacher")]
        [HttpGet]
        [ProducesResponseType (StatusCodes.Status200OK)]
        public IActionResult GetAll ()
            {
            _logger.LogInformation ("GET /students called");
            return Ok (_service.GetAll ());
            }

        [Authorize (Roles = "Teacher")]
        [HttpGet ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public IActionResult GetById ( int id )
            {
            _logger.LogInformation ("GET /students/{Id} called", id);
            return Ok (_service.GetById (id));
            }

        [Authorize (Roles = "Student")]
        [HttpPost]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        public IActionResult Create ( [FromBody] CreateStudentForm form )
            {
            _logger.LogInformation ("POST /students called");
            return Ok (_service.Create (form));
            }

        [Authorize (Roles = "Teacher")]
        [HttpPut ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public IActionResult Update ( int id, [FromBody] UpdateStudentForm form )
            {
            _logger.LogInformation ("PUT /students/{Id} called", id);
            return Ok (_service.Update (id, form));
            }

        [Authorize (Roles = "Teacher")]
        [HttpDelete ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (StatusCodes.Status404NotFound)]
        public IActionResult Delete ( int id )
            {
            _logger.LogInformation ("DELETE /students/{Id} called", id);
            _service.Delete (id);
            return Ok ();
            }
        }
    }