using University.Core.DTOs;
using University.Core.Forms;

namespace University.Core.Services
    {
    public interface IStudentService
        {
        List<StudentDto> GetAll ();
        StudentDto? GetById ( int id );
        StudentDto Create ( CreateStudentForm form );
        StudentDto? Update ( int id, UpdateStudentForm form );
        void Delete ( int id );
        }
    }