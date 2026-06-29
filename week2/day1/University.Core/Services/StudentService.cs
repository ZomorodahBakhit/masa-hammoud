using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Mappings;
using University.Core.Validators;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
    {
    public class StudentService : IStudentService
        {
        private readonly IStudentRepository _repository;
        private readonly ILogger<StudentService> _logger;

        public StudentService ( IStudentRepository repository, ILogger<StudentService> logger )
            {
            _repository = repository;
            _logger = logger;
            }

        public List<StudentDto> GetAll ()
            {
            _logger.LogInformation ("Fetching all students");
            return _repository.GetAll ()
                .Select (s => s.ToDto ())
                .ToList ();
            }

        public StudentDto? GetById ( int id )
            {
            _logger.LogInformation ("Fetching student with id {Id}", id);
            var student = _repository.GetById (id);

            if ( student == null )
                {
                _logger.LogWarning ("Student with id {Id} not found", id);
                throw new NotFoundException ($"Student with id {id} not found");
                }

            return student.ToDto ();
            }

        public StudentDto Create ( CreateStudentForm form )
            {
            _logger.LogInformation ("Attempting to create student with Name: {Name}", form.Name);

            var validationResult = FormValidator.Validate (form);
            if ( !validationResult.IsValid )
                {
                _logger.LogWarning ("Validation failed for student creation. Errors: {Errors}",
                    string.Join (", ", validationResult.Errors.SelectMany (e => e.Value)));
                throw new BusinessException (validationResult.Errors);
                }

            var existingStudent = _repository.GetByEmail (form.Email);
            if ( existingStudent != null )
                {
                _logger.LogWarning ("Duplicate email {Email}", form.Email);
                throw new BusinessException (new Dictionary<string, List<string>>
        {
            { "Email", new List<string> { $"Email {form.Email} is already in use" } }
        });
                }

            var student = new Student { Name = form.Name, Email = form.Email };
            _repository.Add (student);
            _logger.LogInformation ("Student created with id {Id}", student.Id);

            return student.ToDto ();
            }

        public StudentDto? Update ( int id, UpdateStudentForm form )
            {
            _logger.LogInformation ("Attempting to update student with ID: {Id}", id);

            var validationResult = FormValidator.Validate (form);
            if ( !validationResult.IsValid )
                {
                _logger.LogWarning ("Validation failed for student update. Errors: {Errors}",
                    string.Join (", ", validationResult.Errors.SelectMany (e => e.Value)));
                throw new BusinessException (validationResult.Errors);
                }

            var student = _repository.GetById (id);
            if ( student == null )
                {
                _logger.LogWarning ("Student with ID: {Id} not found", id);
                throw new NotFoundException ($"Student with id {id} not found");
                }

            var existingStudent = _repository.GetByEmail (form.Email);
            if ( existingStudent != null && existingStudent.Id != id )
                {
                _logger.LogWarning ("Duplicate email {Email}", form.Email);
                throw new BusinessException (new Dictionary<string, List<string>>
        {
            { "Email", new List<string> { $"Email {form.Email} is already in use" } }
        });
                }

            student.Name = form.Name;
            student.Email = form.Email;
            _repository.Update (student);
            _logger.LogInformation ("Student with id {Id} updated", id);

            return student.ToDto ();
            }

        public void Delete ( int id )
            {
            var student = _repository.GetById (id);
            if ( student == null )
                {
                _logger.LogWarning ("Student with id {Id} not found", id);
                throw new NotFoundException ($"Student with id {id} not found");
                }

            _repository.Delete (student);
            _logger.LogInformation ("Student with id {Id} deleted", id);

            }
        }
    }