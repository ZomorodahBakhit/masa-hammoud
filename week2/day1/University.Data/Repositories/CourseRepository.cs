using University.Data.Context;
using University.Data.Entities;

namespace University.Data.Repositories
    {
    public class CourseRepository : ICourseRepository
        {
        private readonly UniversityDbContext _context;

        public CourseRepository ( UniversityDbContext context )
            {
            _context = context;
            }

        public List<Course> GetAll ()
            {
            return _context.Courses.ToList ();
            }

        public Course? GetById ( int id )
            {
            return _context.Courses.FirstOrDefault (x => x.Id == id);
            }

        public Course? GetByTitle ( string title )
            {
            return _context.Courses.FirstOrDefault (x => x.Title == title);
            }

        public void Add ( Course course )
            {
            _context.Courses.Add (course);
            _context.SaveChanges ();
            }

        public void Update ( Course course )
            {
            _context.Courses.Update (course);
            _context.SaveChanges ();
            }

        public void Delete ( Course course )
            {
            _context.Courses.Remove (course);
            _context.SaveChanges ();
            }
        }
    }