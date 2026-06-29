using University.Data.Entities;

namespace University.Data.Repositories
    {
    public interface ICourseRepository
        {
        List<Course> GetAll ();
        Course? GetById ( int id );
        Course? GetByTitle ( string title );
        void Add ( Course course );
        void Update ( Course course );
        void Delete ( Course course );
        }
    }