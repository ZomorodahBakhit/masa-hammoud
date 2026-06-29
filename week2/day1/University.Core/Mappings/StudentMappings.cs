using University.Core.DTOs;
using University.Data.Entities;

namespace University.Core.Mappings
    {
    public static class StudentMappings
        {
        public static StudentDto ToDto ( this Student student ) => new ()
            {
            Id = student.Id,
            Name = student.Name,
            Email = student.Email
            };
        }
    }