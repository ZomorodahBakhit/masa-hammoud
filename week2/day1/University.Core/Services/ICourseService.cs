using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services
    {
    public interface ICourseService
        {
        List<CourseDto> GetAll ();
        CourseDto? GetById ( int id );
        CourseDto Create ( CreateCourseForm form );
        CourseDto? Update ( int id, UpdateCourseForm form );
        void Delete ( int id );
        }
    }