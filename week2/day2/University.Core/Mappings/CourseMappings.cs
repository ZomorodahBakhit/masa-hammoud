using University.Core.DTOs;
using University.Data.Entities;

namespace University.Core.Mappings
    {
    public static class CourseMappings
        {
        public static CourseDto ToDto ( this Course course ) => new ()
            {
            Id = course.Id,
            Title = course.Title,
            StartDate = course.StartDate,
            EndDate = course.EndDate
            };
        }
    }