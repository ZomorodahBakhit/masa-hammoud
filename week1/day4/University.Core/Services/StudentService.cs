using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
    {
    public class StudentService : IStudentService
        {
        private readonly IStudentRepository _repository;

        public StudentService ( IStudentRepository repository )
            {
            _repository = repository;
            }

        public List<StudentDto> GetAll ()
            {
            return _repository.GetAll ()
                .Select (s => new StudentDto
                    {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email
                    })
                .ToList ();
            }

        public StudentDto? GetById ( int id )
            {
            var student = _repository.GetById (id);

            if ( student == null )
                return null;

            return new StudentDto
                {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
                };
            }

        public StudentDto Create ( CreateStudentForm form )
            {
            var student = new Student
                {
                Name = form.Name,
                Email = form.Email
                };

            _repository.Add (student);

            return new StudentDto
                {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
                };
            }

        public StudentDto? Update ( int id, UpdateStudentForm form )
            {
            var student = _repository.GetById (id);

            if ( student == null )
                return null;

            student.Name = form.Name;
            student.Email = form.Email;

            _repository.Update (student);

            return new StudentDto
                {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email
                };
            }

        public void Delete ( int id )
            {
            var student = _repository.GetById (id);

            if ( student == null )
                return;

            _repository.Delete (student);
            }
        }
    }