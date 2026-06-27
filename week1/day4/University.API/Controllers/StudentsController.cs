using Microsoft.AspNetCore.Mvc;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _service.GetById(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(CreateStudentForm form)
        {
            var student = _service.Create(form);

            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateStudentForm form)
        {
            var student = _service.Update(id, form);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);

            return Ok();
        }
    }
}