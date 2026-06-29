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
    public class CourseService : ICourseService
        {
        private readonly ICourseRepository _repository;
        private readonly ILogger<CourseService> _logger;

        public CourseService ( ICourseRepository repository, ILogger<CourseService> logger )
            {
            _repository = repository;
            _logger = logger;
            }

        public List<CourseDto> GetAll ()
            {
            _logger.LogInformation ("Fetching all courses");
            return _repository.GetAll ()
                .Select (c => c.ToDto ())
                .ToList ();
            }

        public CourseDto? GetById ( int id )
            {
            _logger.LogInformation ("Fetching course with id {Id}", id);
            var course = _repository.GetById (id);

            if ( course == null )
                {
                _logger.LogWarning ("Course with id {Id} not found", id);
                throw new NotFoundException ($"Course with id {id} not found");
                }

            return course.ToDto ();
            }

        public CourseDto Create ( CreateCourseForm form )
            {
            _logger.LogInformation ("Attempting to create course with Title: {Title}", form.Title);

            var validationResult = FormValidator.Validate (form);
            if ( !validationResult.IsValid )
                {
                _logger.LogWarning ("Validation failed for course creation. Errors: {Errors}",
                    string.Join (", ", validationResult.Errors.SelectMany (e => e.Value)));
                throw new BusinessException (validationResult.Errors);
                }

            var existing = _repository.GetByTitle (form.Title);
            if ( existing != null )
                {
                _logger.LogWarning ("Duplicate title {Title}", form.Title);
                throw new BusinessException (new Dictionary<string, List<string>>
        {
            { "Title", new List<string> { $"Course with title '{form.Title}' already exists" } }
        });
                }

            var course = new Course
                {
                Title = form.Title,
                StartDate = form.StartDate,
                EndDate = form.EndDate
                };

            _repository.Add (course);
            _logger.LogInformation ("Course created with id {Id}", course.Id);

            return course.ToDto ();
            }

        public CourseDto? Update ( int id, UpdateCourseForm form )
            {
            _logger.LogInformation ("Attempting to update course with ID: {Id}", id);

            var validationResult = FormValidator.Validate (form);
            if ( !validationResult.IsValid )
                {
                _logger.LogWarning ("Validation failed for course update. Errors: {Errors}",
                    string.Join (", ", validationResult.Errors.SelectMany (e => e.Value)));
                throw new BusinessException (validationResult.Errors);
                }

            var course = _repository.GetById (id);
            if ( course == null )
                {
                _logger.LogWarning ("Course with ID: {Id} not found", id);
                throw new NotFoundException ($"Course with id {id} not found");
                }

            var existing = _repository.GetByTitle (form.Title);
            if ( existing != null && existing.Id != id )
                {
                _logger.LogWarning ("Duplicate title {Title}", form.Title);
                throw new BusinessException (new Dictionary<string, List<string>>
        {
            { "Title", new List<string> { $"Course with title '{form.Title}' already exists" } }
        });
                }

            course.Title = form.Title;
            course.StartDate = form.StartDate;
            course.EndDate = form.EndDate;

            _repository.Update (course);
            _logger.LogInformation ("Course with id {Id} updated", id);

            return course.ToDto ();
            }

        public void Delete ( int id )
            {
            var course = _repository.GetById (id);
            if ( course == null )
                {
                _logger.LogWarning ("Course with id {Id} not found", id);
                throw new NotFoundException ($"Course with id {id} not found");
                }

            _repository.Delete (course);
            _logger.LogInformation ("Course with id {Id} deleted", id);
            }
        }
    }